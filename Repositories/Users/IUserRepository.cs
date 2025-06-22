using ForumWebsite.Models;

namespace ForumWebsite.Repositories.Users
{
    public interface IUserRepository
    {
        Task<User> GetUserById(int id);
        Task<User?> GetByEmailUser(string email);
        Task<User?> GetByUsername(string username);
        Task<ICollection<User>> GetAllUsers();
        Task AddUser(User user);
        Task UpdateUser(User user);
        Task DeleteUser(int id);

        Task UpdateUserReputation(int userId);
    }
}
