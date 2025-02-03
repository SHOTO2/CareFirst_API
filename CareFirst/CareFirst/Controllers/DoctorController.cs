using CareFirst.IRepository;
using CareFirst.Mapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;

namespace CareFirst.Controllers
{
    [Route("Doctor")]
    [ApiController]
    [Authorize]
    public class DoctorController(IDoctorRepository _doctor) : ControllerBase
    {
        [HttpGet("Get", Name = "GetAllDoctors")]
        public async Task<ActionResult> GetAllDoctors([FromQuery] string Department = "")
        {
            int DepartmentID = 0;
            if (Department != "")
            {
                if (Enum.TryParse(Department, out DoctorDepartmentTypes name))
                {
                    DepartmentID = (int)name;
                }
                else
                {
                    return NotFound("Department Name Not Found (404)");
                }
            }
            var listDoctors = await _doctor.GetAllDoctorsAsync(DepartmentID);
            if (listDoctors == null)
                return NotFound("There Are No Doctors (404)");

            var listDoctorDto = listDoctors.Select(Doctor => Doctor.ToDoctorDto());

            return Ok(listDoctorDto);
        }

        [HttpGet("ID/{Id}", Name = "GetDoctorByID")]
        public async Task<ActionResult> GetDoctorByID(int Id)
        {
            if (Id <= 0)
            {
                return BadRequest("You must add an available ID.");
            }
            var doctor = await _doctor.GetDoctorByIdAsync(Id);
            if (doctor == null)
                return NotFound("This doctor is not available.");
            return Ok(doctor.ToDoctorByIdDto());
        }
        [Authorize(Roles ="Admin")]
        [HttpPut("change/{appintmentId}", Name = "ChangeAppointment")]
        public async Task<ActionResult> ChangeAppointment(int appintmentId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("The data is wrong.");
            }
            if (appintmentId == 0)
                return BadRequest("The data is wrong.");

            bool IsChange = await _doctor.ChangeStateAsync(appintmentId);
            if (!IsChange)
                return NotFound("You can't Change State");
            return Ok("Data Change Successfully User Now Can User Add Review");
        }
    }
}
