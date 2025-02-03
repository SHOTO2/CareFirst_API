using CareFirst.Dtos.ReviewDto;
using CareFirst.Dtos.UserDtos;
using CareFirst.Dtos.UsersDto;
using CareFirst.Model;

namespace CareFirst.IRepository
{
    public interface IUserRepository
    {
        Task<UserTable?> AddUserAsync(CreateUserDto newUser);
        Task<UserTable?> LoginUserAsync(LoginUserDto loginUser);
        string? GenerateToken(UserTable user);
        Task<UserTable?> GetInfo(int iD);
        Task<bool> SendEmailAsync(string email);
        //Task<bool> SendPhoneAsync(string phone);
        Task<UserTable?> ResetPassword(ResetPasswordDto reset);
        Task<bool> SendReviewAsync(ReviewDoctorDto review, int userId, int doctorId);
        Task<string> AddImageAsync(IFormFile file, int userId);
        Task<string> DeleteImageAsync(int userId);
        Task<string> UpdateImageAsync(IFormFile file, int userId);

    }
}
