using ForumWebsite.Dtos.Users;

namespace ForumWebsite.Services.Users
{
    public interface IUserService
    {
        Task<UserDto> Register(RegisterDto dto);
        Task<LoginSuccessfullyDto> Login(LoginDto dto); 
        Task<UserDto> GetProfile(int userId);
    }
}
