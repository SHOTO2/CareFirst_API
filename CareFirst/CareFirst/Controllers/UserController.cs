using CareFirst.Dtos.ReviewDto;
using CareFirst.Dtos.UserDtos;
using CareFirst.Dtos.UsersDto;
using CareFirst.IRepository;
using CareFirst.Mapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Numerics;
using System.Security.Claims;

namespace CareFirst.Controllers
{
    [Route("user")]
    [ApiController]
    [Authorize]
    public class UserController(IUserRepository _user, IDoctorRepository _doctor) : ControllerBase
    {

        [AllowAnonymous]
        [HttpPost("Create", Name = "CreateAccount")]
        public async Task<ActionResult> CreateAccount([FromBody] CreateUserDto newUser)
        {
            if (newUser == null)
            {
                return BadRequest("There is an error here, try again.");
            }
            if (newUser.Password != newUser.ConfirmPassword)
            {
                return BadRequest("Check that the password and confirm  password are the same.");
            }
            var user = await _user.AddUserAsync(newUser);
            if (user == null)
                return BadRequest("Invalid user data.");

            var response = new
            {
                Token = _user.GenerateToken(user)
            };

            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("Login", Name = "LoginUser")]
        public async Task<ActionResult> LoginUser([FromBody] LoginUserDto loginUser)
        {
            if (loginUser == null)
            {
                return BadRequest("There is an error here, try again.");
            }
            var user = await _user.LoginUserAsync(loginUser);
            if (user == null)
                return BadRequest("Invalid user data.");

            var response = new
            {
                Token = _user.GenerateToken(user)
            };
            return Ok(response);
        }

        [HttpGet("Info", Name = "GetUserInfo")]
        [Authorize]
        public async Task<ActionResult> GetUserInfo()
        {
            var userId = int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var user = await _user.GetInfo(userId);
            if (user == null)
                return NotFound("There is an error here, try again.");
            return Ok(user.ToUserDto());

        }

        [AllowAnonymous]
        [HttpPost("Forget/password/email/{email}", Name = "VerifiedPasswordbyEmail")]
        public async Task<ActionResult> VerifiedPasswordbyEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                return BadRequest();
            bool IsSend = await _user.SendEmailAsync(email);
            if (!IsSend)
                return NotFound("There is an error here, try again.");
            return Ok("Check your email");
        }

        [AllowAnonymous]
        [HttpPost("Forget/password/phone/{phone}", Name = "VerifiedPasswordbyPhone")]
        public async Task<ActionResult> VerifiedPasswordbyPhone(string phone)
        {
            if (string.IsNullOrEmpty(phone))
                return BadRequest();
            //bool IsSend = await _user.SendPhoneAsync(phone);
            //if (!IsSend)
                return NotFound("There is an error here, try again.");
            //return Ok("Check your phone");
        }

        [AllowAnonymous]
        [HttpPost("ResetPassword", Name = "ResetPassword")]
        public async Task<ActionResult> ResetPassword([FromBody] ResetPasswordDto resetPassword)
        {
            if (resetPassword == null)
                return NotFound();
            var user = await _user.ResetPassword(resetPassword);
            if (user == null)
                return NotFound("There is an error here, try again.");

            var response = new
            {
                Token = _user.GenerateToken(user)
            };
            return Ok(response);
        }

        [HttpPatch("Review/{DoctorId}", Name = "AddReviewToDoctor")]
        public async Task<ActionResult> AddReviewToDoctor([FromBody] ReviewDoctorDto review, int DoctorId)
        {
            if (_doctor.GetDoctorByIdAsync(DoctorId) == null)
                return BadRequest("No Doctor Have This Id");
            if (review == null)
                return BadRequest("Must Fill review");
            var userId = int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            bool IsSendReview = await _user.SendReviewAsync(review, userId, DoctorId);

            if (!IsSendReview)
                return NotFound("You can't Add Review To Doctor Must Visit Doctor First");
            return Ok("Review sent thank you");
        }
        [HttpPost("picture", Name = "AddProfilePicture")]
        public async Task<ActionResult> AddProfilePicture(IFormFile file)
        {
            var userId = int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var opration = await _user.AddImageAsync(file, userId);
            if (opration != "")
                return BadRequest(opration);
            return Ok("Image Add Successfully");
        }

        [HttpDelete("picture", Name = "DeleteProfilePicture")]
        public async Task<ActionResult> DeleteProfilePicture()
        {
            var userId = int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var opration = await _user.DeleteImageAsync(userId);
            if (opration != "")
                return BadRequest(opration);
            return Ok("Image delete Successfully");
        }

        [HttpPut("picture", Name = "UpdateProfilePicture")]
        public async Task<ActionResult> UpdateProfilePicture(IFormFile file)
        {
            var userId = int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var opration = await _user.UpdateImageAsync(file, userId);
            if (opration != "")
                return BadRequest(opration);
            return Ok("Image Update Successfully");
        }
    }
}
