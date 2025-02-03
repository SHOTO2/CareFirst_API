namespace CareFirst.Dtos.UsersDto
{
    public class ResetPasswordDto
    {
        public string Token { set; get; }=string.Empty;
        public string Password { get; set; } = null!;
        public string ConfirmPassword { get; set; } = null!;
    }
}
