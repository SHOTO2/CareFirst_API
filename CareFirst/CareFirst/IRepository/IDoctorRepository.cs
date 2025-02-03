using CareFirst.Model;

namespace CareFirst.IRepository
{
    public interface IDoctorRepository
    {
        Task<IEnumerable<Doctor>?> GetAllDoctorsAsync(int departmentId);
        Task<Doctor?> GetDoctorByIdAsync(int doctorId);
        Task<bool> ChangeStateAsync(int appintmentId);
    }
}
