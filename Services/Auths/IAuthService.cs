using ForumWebsite.Dtos.Auths;

namespace ForumWebsite.Services.Auths
{
    public interface IAuthService
    {
        Task<AuthUserDto> Register(RegisterDto dto);
        Task<LoginSuccessfullyDto> Login(LoginDto dto); 
        Task<AuthUserDto> GetProfile(int userId);
    }
}
