using CareFirst.Context;
using CareFirst.IRepository;
using CareFirst.Model;
using Microsoft.EntityFrameworkCore;

namespace CareFirst.Repository
{
    public class NurseRepository(ApplicationDbContext _context) : INurseRepository
    {
        public async Task<IEnumerable<Nurse>?> GetAllNursesAsync(int departmentId)
        {
            List<Nurse> listNurses = (departmentId == 0) ?
            await _context.Nurses.ToListAsync() :
            await _context.Nurses.Where(X => X.DepartmentId == departmentId).ToListAsync();

            if (listNurses == null)
                return null;
            return listNurses;
        }

        public async Task<Nurse?> GetNurseByIdAsync(int nurseId)
        {
            var nurse = await _context.Nurses.FirstOrDefaultAsync(X => X.NurseId == nurseId);
            if (nurse == null)
                return null;
            return nurse;
        }
    }
}
