using ForumWebsite.Datas;
using ForumWebsite.Models;
using ForumWebsite.Repositories.Users;
using Microsoft.EntityFrameworkCore;

namespace ForumWebsite.Repositories.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id)
              ?? throw new KeyNotFoundException("key is null");

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

        }

        public async Task<ICollection<User>> GetAllUsers()
        {
            var users = await _context.Users
                .Include(u => u.Comments)
                .Include(u => u.CommentVotes)
                .Include(u => u.ForumThreads)
                .Include(u => u.ForumThreadVotes)
                .ToListAsync();
            return users;
        }

        public async Task<User> GetUserById(int id)
        {
            //var user = await _context.Users.FindAsync(id)
            //              ?? throw new KeyNotFoundException("key is null");
            var user = await _context.Users
                .Where(u => u.Id == id)
                .Include(u => u.Comments)
                .Include(u => u.CommentVotes)
                .Include(u => u.ForumThreads)
                .Include(u => u.ForumThreadVotes)
                .FirstOrDefaultAsync()
                ?? throw new KeyNotFoundException("key is null");

            return user;
        }

        public async Task UpdateUser(User user)
        {
            var existingUser = await _context.Users.FindAsync(user.Id)
                          ?? throw new KeyNotFoundException("key is null");

            _context.Entry(existingUser).CurrentValues.SetValues(user);
            await _context.SaveChangesAsync();
        }
        public async Task<User?> GetByEmailUser(string email)
        {
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email);
            return existingUser;
        }
        public async Task<User?> GetByUsername(string username)
        {
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == username);
            return existingUser;
        }

        public async Task UpdateUserReputation(int userId)
        {
            var threadReputation = await _context.ForumThreads
                .Where(t => t.UserId == userId)
                .SumAsync(t => (int?)t.Reputation) ?? 0;

            var commentReputation = await _context.Comments
                .Where(c => c.UserId == userId)
                .SumAsync(c => (int?)c.Reputation) ?? 0;

            var totalReputation = Math.Max(0, threadReputation + commentReputation);

            var user = await _context.Users.FindAsync(userId);
            if (user != null)
            {
                user.Reputation = totalReputation;
                await _context.SaveChangesAsync();
            }
        }

        
    }
}

