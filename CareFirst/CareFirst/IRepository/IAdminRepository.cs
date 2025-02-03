using CareFirst.Dtos.AdminDto;
using CareFirst.Model;

namespace CareFirst.IRepository
{
    public interface IAdminRepository
    {
        Task<SupperUser?> CreateAdminAsync(CreateDto newAdmin);
        Task<SupperUser?> LoginAdminAsync(LoginDto login);
        string? GenerateToken(SupperUser admin);
    }
}
