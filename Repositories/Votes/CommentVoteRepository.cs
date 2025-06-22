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
            var vote = await _context.CommentVotes.FirstOrDefaultAsync(cv => cv.UserId == userId && cv.CommentId == commentId);
            
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
        
        public async Task<ICollection<User>> GetUserVoteUpComment(int commentId)
        {
            var userVoteUp = await _context.CommentVotes
                .Where(cv => cv.CommentId == commentId && cv.IsUpvote)
                .Select(cv => cv.User)
                .ToListAsync();
            return userVoteUp;
        }
        public async Task<ICollection<User>> GetUserVoteDownComment(int commentId)
        {
            var userVoteDown = await _context.CommentVotes
                .Where(cv => cv.CommentId == commentId && !cv.IsUpvote)
                .Select (cv => cv.User)
                .ToListAsync();
            return userVoteDown;
        }

        public async Task UpdateCommentReputation(int commentId)
        {
            var up = await _context.CommentVotes.CountAsync(v => v.CommentId == commentId && v.IsUpvote);
            var down = await _context.CommentVotes.CountAsync(v => v.CommentId == commentId && !v.IsUpvote);

            var comment = await _context.Comments.FindAsync(commentId);
            if (comment != null)
            {
                comment.Reputation = Math.Max(0, up - down);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<int?> GetCommentOwnerId(int commentId)
        {
            return await _context.Comments
                .Where(c => c.Id == commentId)
                .Select(c => (int?)c.UserId)
                .FirstOrDefaultAsync();
        }

    }
}
