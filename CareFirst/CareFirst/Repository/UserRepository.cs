using CareFirst.Context;
using CareFirst.Dtos.UserDtos;
using CareFirst.IRepository;
using CareFirst.Model;
using CareFirst.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using MailKit.Security;
using MimeKit;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;
using CareFirst.Dtos.UsersDto;
using CareFirst.Dtos.ReviewDto;
using Microsoft.AspNetCore.Components.Routing;
using MimeKit.Cryptography;

namespace CareFirst.Repository
{

    public class UserRepository
        (ApplicationDbContext _context, IPasswordHash _hash, JwtOptions _jwt, SmtpOptions _smtp, TwilioOptions _twil)
        : IUserRepository
    {
        private string _createRandom5Number()
        {
            Random random = new Random();
            int randomNumber = random.Next(10000, 100000);
            return randomNumber.ToString();
        }
        private async Task<UserTable?> _getUserByEmail(string email)
        {
            var user = await _context.UserTables.FirstOrDefaultAsync(X => X.Email == email);
            if (user == null)
                return null;
            return user;
        }
        private async Task<UserTable?> _getUserByPhone(string phone)
        {
            var user = await _context.UserTables.FirstOrDefaultAsync(X => X.PhoneNumber == phone);
            if (user == null)
                return null;
            return user;
        }
        private async Task GenerateResetToken(UserTable user)
        {
            var randomOTP = _createRandom5Number();
            user.PasswordResetToken = randomOTP;
            user.ResetTokenExpires = DateTime.Now.AddDays(1);
            await _context.SaveChangesAsync();
        }
        private async Task<UserTable?> GetUserByToken(string Token)
        {
            var user = await _context.UserTables.FirstOrDefaultAsync(X => X.PasswordResetToken == Token);
            if (user == null) return null;
            return user!;
        }
        private async Task<int> _getAppointmentDoctor(int userId, int doctorId)
        {
            var userIsVisit = await _context.AppointmentDoctors
                .Where(X => X.UserId == userId && X.DoctorId == doctorId && X.Status == "visited")
                .ToListAsync();

            if (userIsVisit.Count == 0) return 0;

            var appointmnet = userIsVisit.LastOrDefault();
            if (appointmnet == null) return 0;

            appointmnet.Status = "Finished";
            try
            {
                _context.SaveChanges();
            }
            catch
            {
                _context.Dispose();
                return 0;
            }
            return appointmnet.AppointmentId;


        }

        public async Task<UserTable?> AddUserAsync(CreateUserDto newUser)
        {
            var user = new UserTable()
            {
                Age = newUser.Age,
                Email = newUser.Email,
                Gender = newUser.Gender,
                Name = newUser.Name,
                PhoneNumber = newUser.PhoneNumber,
                PasswordHash = _hash.Hash(newUser.Password),
            };
            try
            {
                await _context.UserTables.AddAsync(user);
                await _context.SaveChangesAsync();
            }
            catch
            {
                return null;
            }
            return user;
        }

        public string? GenerateToken(UserTable user)
        {
            var Claims = new List<Claim>()
            {
                    new (ClaimTypes.NameIdentifier,user.UserId.ToString()),
                    new (ClaimTypes.Name,user.Name),
                    new (ClaimTypes.Email,user.Email),
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Issuer = _jwt.Issuer,
                Audience = _jwt.Audiennce,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey
                (Encoding.UTF8.GetBytes(_jwt.SigningKey!)), SecurityAlgorithms.HmacSha256),
                Subject = new ClaimsIdentity(Claims),
            };
            var securitytoken = tokenHandler.CreateToken(tokenDescriptor);
            var accessToken = tokenHandler.WriteToken(securitytoken);
            return accessToken;
        }

        public async Task<UserTable?> GetInfo(int iD)
        {
            var user = await _context.UserTables.FirstOrDefaultAsync(X => X.UserId == iD);
            if (user == null)
                return null;
            return user;
        }

        public async Task<UserTable?> LoginUserAsync(LoginUserDto loginUser)
        {

            var user = await _context.UserTables.FirstOrDefaultAsync(X => loginUser.Email == X.Email.ToString());
            if (user == null)
                return null;

            bool Verified = _hash.Verified(user.PasswordHash, loginUser.Password);
            if (!Verified)
            {
                return null;
            }
            return user;
        }

        public async Task<bool> SendEmailAsync(string email)
        {
            var user = await _getUserByEmail(email);
            if (user == null)
                return false;
            await GenerateResetToken(user);

            try
            {
                string otp = user.PasswordResetToken!;

                var sender = new MimeKit.MimeMessage();
                sender.From.Add(MailboxAddress.Parse(_smtp.EmailSend));
                sender.To.Add(MailboxAddress.Parse(user.Email));
                sender.Subject = "The Token To verify";
                sender.Subject = $"The Token : ${otp}";
                sender.Body = new TextPart("plain") { Text = $"The Token : {otp}" };

                using var smtp = new SmtpClient();
                smtp.Connect(_smtp.SmtpServer, _smtp.Port, SecureSocketOptions.SslOnConnect);
                smtp.Authenticate(_smtp.EmailSend, _smtp.AppPassword);
                smtp.Send(sender);
                smtp.Disconnect(true);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> SendPhoneAsync(string phone)
        {
            var user = await _getUserByPhone(phone);
            if (user == null)
                return false;
            await GenerateResetToken(user);

            try
            {
                TwilioClient.Init(_twil.AccountSid, _twil.Authtoken);
                string otp = user.PasswordResetToken!;

                var message = MessageResource.Create(
                    body: $"Your OTP code is: {otp}",
                    from: new PhoneNumber(_twil.TwilioPhoneNumber),
                    to: new PhoneNumber($"+20{user.PhoneNumber}")
                );

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<UserTable?> ResetPassword(ResetPasswordDto reset)
        {
            var user = await GetUserByToken(reset.Token);
            if (user == null) return null;

            if (reset.Password != reset.ConfirmPassword && user.ResetTokenExpires < DateTime.Now)
                return null;
            user.PasswordHash = _hash.Hash(reset.Password);
            user.PasswordResetToken = null;
            user.ResetTokenExpires = null;

            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<bool> SendReviewAsync(ReviewDoctorDto review, int userId, int doctorId)
        {
            int appointId = await _getAppointmentDoctor(userId, doctorId);
            if (appointId == 0) return false;

            var newreview = new ReviewDoctor()
            {
                DoctorId = doctorId,
                AppointmentDoctorId = appointId,
                Rating = review.Rating,
                Comment = review.Comment,
            };

            try
            {
                await _context.ReviewDoctors.AddAsync(newreview);
                await _context.SaveChangesAsync();

            }
            catch
            {
                await _context.DisposeAsync();
                return false;
            }
            return true;
        }

        public async Task<string> AddImageAsync(IFormFile file, int userId)
        {
            List<string> validExtention = new List<string>() { ".jpg", ".png", ".jpeg" };

            if (file.Length > (5 * 1024 * 1024))
            {
                return "Must Upload Image Not Larger Than 5mb";
            }

            var contantPath = Directory.GetCurrentDirectory();
            string path = Path.Combine(contantPath, "root");

            if (File.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var ext = Path.GetExtension(file.FileName);
            if (!validExtention.Contains(ext))
            {
                return $"Only {string.Join(',', validExtention)}";
            }

            var fileName = $"{Guid.NewGuid().ToString()}{ext}";
            var fileNameWithPath = Path.Combine(path, fileName);
            using var stream = new FileStream(fileNameWithPath, FileMode.Create);
            await file.CopyToAsync(stream);

            var user = await _context.UserTables.FirstOrDefaultAsync(x => x.UserId == userId);
            if (user == null) return "User Not Found";
            user.ProfilePicture = fileNameWithPath;
            await _context.SaveChangesAsync();
            return "";

        }
        public async Task<string> DeleteImageAsync(int userId)
        {
            var user = await _context.UserTables.FirstOrDefaultAsync(x => x.UserId == userId);
            if (user == null) return "User Not Found";
            var ImagePath = user.ProfilePicture;
            if (string.IsNullOrEmpty(ImagePath))
            {
                return "No Image To Delete";
            }
            var contantPath = Directory.GetCurrentDirectory();
            string path = Path.Combine(contantPath, $"root", ImagePath);

            if (!File.Exists(path))
            {
                return "Invalide file Path";
            }
            File.Delete(path);

            user.ProfilePicture = null;
            await _context.SaveChangesAsync();
            return "";
        }
        public async Task<string> UpdateImageAsync(IFormFile file, int userId)
        {
            var user = await _context.UserTables.FirstOrDefaultAsync(x => x.UserId == userId);
            if (user == null) return "User Not Found";
            var OldImage = user.ProfilePicture;

            if (!string.IsNullOrEmpty(OldImage))
            {
                var contantDeletePath = Directory.GetCurrentDirectory();
                string pathDelete = Path.Combine(contantDeletePath, $"root", OldImage);

                if (!File.Exists(pathDelete))
                {
                    return "Invalide file Path";
                }
                File.Delete(pathDelete);

                user.ProfilePicture = null;
            }


            List<string> validExtention = new List<string>() { ".jpg", ".png", ".jpeg" };

            if (file.Length > (5 * 1024 * 1024))
            {
                return "Must Upload Image Not Larger Than 5mb";
            }

            var contantPath = Directory.GetCurrentDirectory();
            string path = Path.Combine(contantPath, "root");

            if (File.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var ext = Path.GetExtension(file.FileName);
            if (!validExtention.Contains(ext))
            {
                return $"Only {string.Join(',', validExtention)}";
            }
            var fileName = $"{Guid.NewGuid().ToString()}{ext}";
            var fileNameWithPath = Path.Combine(path, fileName);
            using var stream = new FileStream(fileNameWithPath, FileMode.Create);
            await file.CopyToAsync(stream);

            user.ProfilePicture = fileNameWithPath;
            await _context.SaveChangesAsync();
            return "";
        }
    }
}
