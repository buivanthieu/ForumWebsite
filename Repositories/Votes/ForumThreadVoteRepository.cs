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
            var vote = await _context.ForumThreadVotes.FirstOrDefaultAsync(cv => cv.UserId == userId && cv.ForumThreadId == forumThreadId);
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
        public async Task<int> GetCountUpVoteForumThread(int forumThreadId)
        {
            var upVotes = await _context.ForumThreadVotes
                .CountAsync(v => v.ForumThreadId == forumThreadId && v.IsUpvote);
            return upVotes;
        }
        public async Task<int> GetCountDownVoteForumThread(int forumThreadId)
        {
            var downVotes = await _context.ForumThreadVotes
                .CountAsync(v => v.ForumThreadId == forumThreadId && !v.IsUpvote);
            return downVotes;
        }
        public async Task<ICollection<User>> GetUserVoteUpThread(int forumThreadId)
        {
            var userList = await _context.ForumThreadVotes
                .Where(v => v.ForumThreadId == forumThreadId && v.IsUpvote)
                .Select(v => v.User)
                .ToListAsync();
            return userList;
        }
        public async Task<ICollection<User>> GetUserVoteDownThread(int forumThreadId)
        {
            var userList = await _context.ForumThreadVotes
                .Where(v => v.ForumThreadId == forumThreadId && !v.IsUpvote)
                .Select(v => v.User)
                .ToListAsync();
            return userList;
        }

        public async Task UpdateForumThreadReputation(int threadId)
        {
            var up = await _context.ForumThreadVotes.CountAsync(v => v.ForumThreadId == threadId && v.IsUpvote);
            var down = await _context.ForumThreadVotes.CountAsync(v => v.ForumThreadId == threadId && !v.IsUpvote);

            var thread = await _context.ForumThreads.FindAsync(threadId);
            if (thread != null)
            {
                thread.Reputation = Math.Max(0, up - down);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<int?> GetForumThreadOwnerId(int threadId)
        {
            return await _context.ForumThreads
                .Where(c => c.Id == threadId)
                .Select(c => (int?)c.UserId)
                .FirstOrDefaultAsync();
        }
    }
}
