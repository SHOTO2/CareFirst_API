using CareFirst.Dtos.BookingDto;
using CareFirst.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CareFirst.Controllers
{
    [Route("Booking")]
    [ApiController]
    [Authorize]
    public class BookingController(IBookingRepository _booking) : ControllerBase
    {
        [HttpPost("Doctor/{Id}", Name = "UserBookingDoctor")]
        public async Task<ActionResult> UserBookingDoctor([FromBody] BookingDoctorDto bookDoctor, int Id)
        {
            if (bookDoctor == null || Id <= 0)
            {
                return BadRequest("The entered data is incorrect.");
            }
            var userId = int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            bool IsBook = await _booking.IsBookDoctor(bookDoctor, Id, userId);

            if (!IsBook)
                return BadRequest("The booking is complete.");

            return Ok(@"The boking has been successfully made.
                        The center will get in touch with you shortly");
        }
        [HttpPost("Nurse/{Id}", Name = "UserBookingNurse")]
        public async Task<ActionResult> UserBookingNurse([FromBody] BookingNurseDto bookNurse, int Id)
        {
            if (bookNurse == null || Id <= 0)
            {
                return BadRequest("The entered data is incorrect.");
            }
            var userId = int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            bool IsBook = await _booking.IsBookNurse(bookNurse, Id, userId);

            if (!IsBook)
                return BadRequest("The booking is complete.");

            return Ok(@"The boking has been successfully made.
                        The center will get in touch with you shortly");
        }
    }
}
