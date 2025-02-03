using CareFirst.Dtos.BookingDto;

namespace CareFirst.IRepository
{
    public interface IBookingRepository
    {
        Task<bool> IsBookDoctor(BookingDoctorDto bookDoctor, int doctorId, int userId);
        Task<bool> IsBookNurse(BookingNurseDto bookNurse, int nurseId, int userId);
    }
}
