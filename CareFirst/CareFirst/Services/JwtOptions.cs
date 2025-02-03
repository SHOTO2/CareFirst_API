namespace CareFirst.Services
{
    public class JwtOptions
    {
        public string? Issuer { get; set; }
        public string? Audiennce { get; set; }
        public int? Lifetime { get; set; }
        public string? SigningKey { get; set; }

    }
}
