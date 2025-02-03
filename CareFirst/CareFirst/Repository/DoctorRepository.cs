using CareFirst.Context;
using CareFirst.IRepository;
using CareFirst.Model;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace CareFirst.Repository
{
    public class DoctorRepository(ApplicationDbContext _contxt) : IDoctorRepository
    {
        public async Task<bool> ChangeStateAsync(int appintmentId)
        {
            var appintment = await _contxt.AppointmentDoctors.FirstOrDefaultAsync(X => X.AppointmentId == appintmentId);
            if (appintment == null)
                return false;
            appintment.Status = "visited";
            await _contxt.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Doctor>?> GetAllDoctorsAsync(int departmentId)
        {
            List<Doctor> listDoctors = (departmentId == 0) ?
                await _contxt.Doctors.ToListAsync() :
                await _contxt.Doctors.Where(X => X.DepartmentId == departmentId).ToListAsync();

            if (listDoctors == null)
                return null;
            return listDoctors;
        }

        public async Task<Doctor?> GetDoctorByIdAsync(int doctorId)
        {
            var doctor = await _contxt.Doctors.FirstOrDefaultAsync(X => X.DoctorId == doctorId);
            if (doctor == null)
                return null;
            return doctor;
        }
    }
}
