using System.ComponentModel.DataAnnotations;

namespace CareFirst.Dtos.UserDtos
{
    public class CreateUserDto
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string ConfirmPassword { get; set; } = null!;
        public int Gender { get; set; }
        public int Age { get; set; }
    }
}
