using ForumWebsite.Datas;
using ForumWebsite.Models;
using Microsoft.EntityFrameworkCore;

namespace ForumWebsite.Repositories.Votes
{
    public class ForumThreadVoteRepository : IForumThreadVoteRepository
    {
        private readonly ApplicationDbContext _context;

        public ForumThreadVoteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ForumThreadVote> GetForumThreadVote(int userId, int forumThreadId)
        {
            var vote = await _context.ForumThreadVotes.FirstOrDefaultAsync(cv => cv.UserId == userId && cv.ForumThreadId == forumThreadId)
                       ?? throw new KeyNotFoundException("key is null");
            return vote;
        }

        public async Task AddForumThreadVote(ForumThreadVote vote)
        {
            await _context.ForumThreadVotes.AddAsync(vote);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateForumThreadVote(ForumThreadVote vote)
        {
            _context.ForumThreadVotes.Update(vote);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteForumThreadVote(ForumThreadVote vote)
        {
            _context.ForumThreadVotes.Remove(vote);
            await _context.SaveChangesAsync();
        }
    }
}
