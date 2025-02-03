using CareFirst.Dtos.UserDtos;
using CareFirst.Model;

namespace CareFirst.Mapper
{
    public static class UserMapper
    {
        public static UserDto ToUserDto(this UserTable user)
        {
            return new UserDto()
            {
                Name = user.Name,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                Gender = user.Gender,
                Age = user.Age,

            };
        }
    }
}
