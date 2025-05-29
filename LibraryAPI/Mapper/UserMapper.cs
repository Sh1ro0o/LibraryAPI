using LibraryAPI.Dto.User;
using LibraryAPI.Model;

namespace LibraryAPI.Mapper
{
    public static class UserMapper
    {
        public static UserDto ToUserDto(this AppUser user)
        {
            return new UserDto
            {
                Id = user.Id,
                Email = user.Email ?? "",
            };
        }
    }
}
