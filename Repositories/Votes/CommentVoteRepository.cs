using ForumWebsite.Datas;
using ForumWebsite.Models;
using Microsoft.EntityFrameworkCore;

namespace ForumWebsite.Repositories.Votes
{
    public class CommentVoteRepository : ICommentVoteRepository
    {
        private readonly ApplicationDbContext _context;

        public CommentVoteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CommentVote> GetCommentVote(int userId, int commentId)
        {
            var vote = await _context.CommentVotes.FirstOrDefaultAsync(cv => cv.UserId == userId && cv.CommentId == commentId)
                       ?? throw new KeyNotFoundException("key is null");
            return vote;
        }

        public async Task AddCommentVote(CommentVote vote)
        {
            await _context.CommentVotes.AddAsync(vote);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCommentVote(CommentVote vote)
        {
            _context.CommentVotes.Update(vote);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCommentVote(CommentVote vote)
        {
            _context.CommentVotes.Remove(vote);
            await _context.SaveChangesAsync();
        }
    }
}
