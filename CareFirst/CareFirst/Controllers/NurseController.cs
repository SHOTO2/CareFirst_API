using CareFirst.IRepository;
using CareFirst.Mapper;
using CareFirst.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CareFirst.Controllers
{
    [Route("Nurse")]
    [ApiController]
    [Authorize]
    public class NurseController(INurseRepository _nurse) : ControllerBase
    {
        [HttpGet("Get", Name = "GetAllNurses")]
        public async Task<ActionResult> GetAllNurses([FromQuery] string Department = "")
        {
            int DepartmentID = 0;
            if (Department != "")
            {
                if (Enum.TryParse(Department, out NurceDepartmentTypes name))
                {
                    DepartmentID = (int)name;
                }
                else
                {
                    return NotFound("Department Name Not Found (404)");
                }
            }
            var listNurses = await _nurse.GetAllNursesAsync(DepartmentID);
            if (listNurses == null)
                return NotFound("There Are No Nurse (404)");

            var listNursesDto = listNurses.Select(Nurse => Nurse.ToNurseDto());

            return Ok(listNursesDto);
        }

        [HttpGet("ID/{Id}", Name = "GetNurseByID")]
        public async Task<ActionResult> GetNurseByID(int Id)
        {
            if (Id <= 0)
            {
                return BadRequest("You must add an available ID.");
            }
            var Nurse = await _nurse.GetNurseByIdAsync(Id);
            if (Nurse == null)
                return NotFound("This Nurse is not available.");
            return Ok(Nurse.ToNurseByIdDto());
        }
    }
}
