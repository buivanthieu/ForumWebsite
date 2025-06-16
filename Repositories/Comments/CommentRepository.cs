using ForumWebsite.Datas;
using ForumWebsite.Models;
using Microsoft.EntityFrameworkCore;

namespace ForumWebsite.Repositories.Comments
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _context;
        public CommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddComment(Comment comment)
        {
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteComment(int id)
        {
            var comment = await _context.Comments.FindAsync(id) 
              ?? throw new KeyNotFoundException("key is null");

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();

        }

        public async Task<ICollection<Comment>> GetAllComments()
        {
            var comments = await _context.Comments.ToListAsync();
            return comments;
        }

        public async Task<Comment> GetCommentById(int id)
        {
            var comment = await _context.Comments.FindAsync($"{id}")
                          ?? throw new KeyNotFoundException("key is null");

            return comment;
        }

        public async Task UpdateComment(Comment comment)
        {
            var existingComment = await _context.Comments.FindAsync(comment.Id)
                          ?? throw new KeyNotFoundException("key is null");

            _context.Entry(existingComment).CurrentValues.SetValues(comment);
            await _context.SaveChangesAsync();
        }
        public async Task<ICollection<Comment>> GetCommentsByThreadId(int threadId)
        {
            return await _context.Comments
                .Where(c => c.ThreadId == threadId)
                .Include(c => c.User)
                .Include(c => c.Votes)
                .ToListAsync();
        }
    }
}
