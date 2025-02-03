using CareFirst.Context;
using CareFirst.Dtos.BookingDto;
using CareFirst.IRepository;
using CareFirst.Model;
using Microsoft.EntityFrameworkCore;

namespace CareFirst.Repository
{
    public class BookingRepository(ApplicationDbContext _context) : IBookingRepository
    {
        private async Task<AppointmentDoctor?> _appointmentToDoctorAsync(int userId, int doctorID)
        {
            var newAppointments = new AppointmentDoctor()
            {
                UserId = userId,
                DoctorId = doctorID,
                Status = "Pending",
                AppointmentDate = DateTime.Now,
            };

            try
            {
                await _context.AppointmentDoctors.AddAsync(newAppointments);
                await _context.SaveChangesAsync();
            }
            catch
            {
                await _context.DisposeAsync();
                return null;
            }
            return newAppointments;
        }
        private async Task<AppointmentNurse?> _appointmentToNurseAsync(int userId, int nurseId)
        {
            var newAppointments = new AppointmentNurse()
            {
                UserId = userId,
                NurseId = nurseId,
                Status = "Pending",
                AppointmentDate = DateTime.Now,
            };

            try
            {
                await _context.AppointmentNurses.AddAsync(newAppointments);
                await _context.SaveChangesAsync();
            }
            catch
            {
                await _context.DisposeAsync();
                return null;
            }
            return newAppointments;
        }
        private bool _checkDoctorAppointments(int doctorId)
        {
            var NumberAppointment = _context.AppointmentDoctors
                .Where(X => X.DoctorId == doctorId)
                .Count();

            var bookAvalible = _context.AppSettings
                .ToList()
                .Select(X => X.MaxBooking)
                .First();

            if (NumberAppointment + 1 > bookAvalible)
                return false;
            return true;
        }
        private bool _checkNurseAppointments(int nurseId)
        {
            var NumberAppointment = _context.AppointmentNurses
                .Where(X => X.NurseId == nurseId)
                .Count();

            var bookAvalible = _context.AppSettings
                .ToList()
                .Select(X => X.MaxBooking)
                .First();

            if (NumberAppointment + 1 > bookAvalible)
                return false;
            return true;
        }


        public async Task<bool> IsBookDoctor(BookingDoctorDto bookDoctor, int doctorId, int userId)
        {
            if (!_checkDoctorAppointments(doctorId))
                return false;


            var newBookDoctor = new BookDoctor()
            {
                DoctorId = doctorId,
                UserId = userId,
                Name = bookDoctor.Name,
                PhoneNumber = bookDoctor.PhoneNumber,
                TimeBook = DateTime.Now,
            };

            try
            {
                await _context.BookDoctors.AddAsync(newBookDoctor);
                await _context.SaveChangesAsync();
            }
            catch
            {
                await _context.DisposeAsync();
                return false;
            }
            var appoint = await _appointmentToDoctorAsync(userId, doctorId);
            if (appoint == null)
                return false;

            return true;
        }

        public async Task<bool> IsBookNurse(BookingNurseDto bookNurse, int nurseId, int userId)
        {
            if (!_checkNurseAppointments(nurseId))
                return false;


            var newBookNurser = new BookNurse()
            {
                Name = bookNurse.Name,
                PhoneNumber = bookNurse.PhoneNumber,
                NurseId = nurseId,
                UserId = userId,
                TimeBook = DateTime.Now,
            };

            try
            {
                await _context.BookNurses.AddAsync(newBookNurser);
                await _context.SaveChangesAsync();
            }
            catch
            {
                await _context.DisposeAsync();
                return false;
            }
            var appoint = await _appointmentToNurseAsync(userId, nurseId);
            if (appoint == null)
                return false;

            return true;
        }
    }
}
