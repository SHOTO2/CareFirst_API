using CareFirst.Dtos.DoctorDto;
using CareFirst.Model;

namespace CareFirst.Mapper
{
    public static class DoctorMapper
    {
        public static DoctorDto ToDoctorDto(this Doctor doctor)
        {
            return new DoctorDto()
            {
                Name = doctor.Name,
                Info = doctor.Info,
                Location = doctor.Location,
                ProfilePicture = doctor.ProfilePicture,
                Rating = doctor.Rating,
            };
        }
        public static DoctorByIdDto ToDoctorByIdDto(this Doctor doctor)
        {
            return new DoctorByIdDto()
            {
                Name = doctor.Name,
                Info = doctor.Info,
                Location = doctor.Location,
                ProfilePicture = doctor.ProfilePicture,
                Rating = doctor.Rating,
                DepartmentName = doctor.Department.Name,
                MedicalCenterName = doctor.MedicalCenter.Name,
            };
        }
    }
}
