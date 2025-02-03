using CareFirst.Context;
using CareFirst.Dtos.AdminDto;
using CareFirst.Dtos.UserDtos;
using CareFirst.IRepository;
using CareFirst.Model;
using CareFirst.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CareFirst.Repository
{
    public class AdminRepository(ApplicationDbContext _context,IPasswordHash _hash ,JwtOptions _jwt) : IAdminRepository
    {
        public async Task<SupperUser?> LoginAdminAsync(LoginDto login)
        {
            var admin = await _context.SupperUsers.FirstOrDefaultAsync(X => login.UserName == X.UserName.ToString());
            if (admin == null)
                return null;

            bool Verified = _hash.Verified(admin.Password!, login.Password!);
            if (!Verified)
            {
                return null;
            }
            return admin;
        }
        public async Task<SupperUser?> CreateAdminAsync(CreateDto newAdmin)
        {
            var admin = new SupperUser()
            {
                
                UserName = newAdmin.UserName,
                Role= "Admin",
                Password = _hash.Hash(newAdmin.Password!),
            };
            try
            {
                await _context.SupperUsers.AddAsync(admin);
                await _context.SaveChangesAsync();
            }
            catch
            {
                return null;
            }
            return admin;
        }
        public string? GenerateToken(SupperUser admin)
        {
            var Claims = new List<Claim>()
            {
                    new (ClaimTypes.NameIdentifier,admin.SupperUserId.ToString()),
                    new (ClaimTypes.Name,admin.UserName!),
                    new (ClaimTypes.Role,admin.Role!),
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
    }
}
