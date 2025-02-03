namespace CareFirst.Dtos.NurseDto
{
    public class NurseByIdDto
    {
        public string Name { get; set; } = null!;
        public string? Info { get; set; }
        public int? Age { get; set; }
        public string? ProfilePicture { get; set; }
        public string? DepartmentName  { get; set; } 
        public string? MedicalCenterName { get; set; } 

    }
}
