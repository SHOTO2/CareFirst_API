using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CareFirst.Dtos.DoctorDto
{
    public class DoctorDto
    {
        public string Name { get; set; } = null!;
        public string? Info { get; set; }
        public decimal? Rating { get; set; }
        public string? Location { get; set; }
        public string? ProfilePicture { get; set; }
    }
}
