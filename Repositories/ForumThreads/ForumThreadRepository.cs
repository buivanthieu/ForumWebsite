using ForumWebsite.Datas;
using ForumWebsite.Models;
using ForumWebsite.Repositories.ForumThreads;
using Microsoft.EntityFrameworkCore;

namespace ForumWebsite.Repositories.ForumThreads
{
    public class ForumThreadRepository : IForumThreadRepository
    {
        private readonly ApplicationDbContext _context;
        public ForumThreadRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddForumThread(ForumThread forumThread)
        {
            _context.ForumThreads.Add(forumThread);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteForumThread(int id)
        {
            var forumThread = await _context.ForumThreads.FindAsync(id)
              ?? throw new KeyNotFoundException("key is null");

            _context.ForumThreads.Remove(forumThread);
            await _context.SaveChangesAsync();

        }

        public async Task<ICollection<ForumThread>> GetAllForumThreads()
        {
            var forumThreads = await _context.ForumThreads.ToListAsync();
            return forumThreads;
        }

        public async Task<ForumThread> GetForumThreadById(int id)
        {
            var forumThread = await _context.ForumThreads.FindAsync($"{id}")
                          ?? throw new KeyNotFoundException("key is null");

            return forumThread;
        }

        public async Task UpdateForumThread(ForumThread forumThread)
        {
            var existingForumThread = await _context.ForumThreads.FindAsync(forumThread.Id)
                          ?? throw new KeyNotFoundException("key is null");

            _context.Entry(existingForumThread).CurrentValues.SetValues(forumThread);
            await _context.SaveChangesAsync();
        }
        public async Task<ICollection<ForumThread>> SearchThreadByName(string name)
        {
            var threads = await _context.ForumThreads.Where(ft => ft.Title.Contains(name))
                .Include(ft => ft.User)
                .Include(ft => ft.Comments)
                .ToListAsync();
            return threads;
        }
    }
}
