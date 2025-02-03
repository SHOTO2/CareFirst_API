

namespace CareFirst.Dtos.DoctorDto
{
    public class DoctorByIdDto
    {
        public string Name { get; set; } = null!;
        public string? Info { get; set; }
        public decimal? Rating { get; set; }
        public string? Location { get; set; }
        public string? ProfilePicture { get; set; }
        public string? DepartmentName  { get; set; } 
        public string? MedicalCenterName { get; set; } 

    }
}
