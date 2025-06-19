using ForumWebsite.Dtos.Users;

namespace ForumWebsite.Services.Users
{
    public interface IUserService
    {
        Task<CurrentUserDto> GetCurrentUser(int userId);
        Task UpdateCurrentUser(UpdateCurrentUserDto updateCurrentUserDto, int userId);


        Task<PublicUserDto> GetUserById(int userId);

        Task<ICollection<AdminUserDto>> GetAllUser();
        Task DeleteUser(int userId);
        Task BanUser(int userId);
    }
}
