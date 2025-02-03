
using CareFirst.Dtos.AdminDto;
using CareFirst.Dtos.UserDtos;
using CareFirst.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CareFirst.Controllers
{
    [Route("Admin")]
    [ApiController]
    public class SupperUserController(IAdminRepository _admin) : ControllerBase
    {
        [AllowAnonymous]
        [HttpPost("LoginAdmin")]
        public async Task<ActionResult> LoginUser([FromBody] LoginDto loginAdmin)
        {
            if (loginAdmin == null)
            {
                return BadRequest("There is an error here, try again.");
            }
            var admin = await _admin.LoginAdminAsync(loginAdmin);
            if (admin == null)
                return BadRequest("Invalid user data.");

            var response = new
            {
                Token = _admin.GenerateToken(admin)
            };
            return Ok(response);
        }
        [AllowAnonymous]
        [HttpPost("CreateAdmin")]
        public async Task<ActionResult> CreateAccount([FromBody] CreateDto newAdmin)
        {
            if (newAdmin == null)
            {
                return BadRequest("There is an error here, try again.");
            }
            var admin = await _admin.CreateAdminAsync(newAdmin);
            if (admin == null)
                return BadRequest("Invalid admin data.");

            var response = new
            {
                Token = _admin.GenerateToken(admin)
            };

            return Ok(response);
        }

    }
}
