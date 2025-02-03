using CareFirst.Model;

namespace CareFirst.IRepository
{
    public interface INurseRepository
    {
        Task<IEnumerable<Nurse>?> GetAllNursesAsync(int departmentId);
        Task<Nurse?> GetNurseByIdAsync(int doctorId);
    }
}
