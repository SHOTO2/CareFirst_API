using CareFirst.Dtos.NurseDto;

using CareFirst.Model;

namespace CareFirst.Mapper
{
    public static class NurseMapper
    {
        public static NurseDto ToNurseDto(this Nurse nurse)
        {
            return new NurseDto()
            {
                Name = nurse.Name,
                Info = nurse.Info,
                Age = nurse.Age,
                ProfilePicture = nurse.ProfilePicture,
            };
        }
        public static NurseByIdDto ToNurseByIdDto(this Nurse nurse)
        {
            return new NurseByIdDto()
            {
                Name = nurse.Name,
                Info = nurse.Info,
                ProfilePicture = nurse.ProfilePicture,
                DepartmentName = nurse.Department!.Name,
                MedicalCenterName = nurse.MedicalCenter!.Name,
            };
        }
    }
}
