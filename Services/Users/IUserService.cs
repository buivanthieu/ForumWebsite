using ForumWebsite.Dtos.Users;

namespace ForumWebsite.Services.Users
{
    public interface IUserService
    {
        Task<UserDto> Register(RegisterDto dto);
        Task<string> Login(LoginDto dto); 
        Task<UserDto> GetProfile(int userId);
    }
}
