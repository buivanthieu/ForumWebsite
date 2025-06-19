using ForumWebsite.Models;
using ForumWebsite.Repositories.Votes;

namespace ForumWebsite.Services.Votes
{
    public class ForumThreadVoteService : IForumThreadVoteService
    {
        private readonly IForumThreadVoteRepository _forumThreadVoteRepository;

        public ForumThreadVoteService (IForumThreadVoteRepository forumThreadVoteRepository)
        {
            _forumThreadVoteRepository = forumThreadVoteRepository;
        }

        public async Task VoteThread(int userId, int threadId, bool isUpVote)
        {
            var existingForumThreadVote = await _forumThreadVoteRepository.GetForumThreadVote(userId, threadId);
            
            if (existingForumThreadVote != null)
            {
                if (existingForumThreadVote.IsUpvote == isUpVote)
                {
                    await _forumThreadVoteRepository.DeleteForumThreadVote(existingForumThreadVote);
                }
                else
                {
                    existingForumThreadVote.IsUpvote = isUpVote;
                    existingForumThreadVote.VotedAt = DateTime.UtcNow;
                    await _forumThreadVoteRepository.UpdateForumThreadVote(existingForumThreadVote);
                }
            }
            else
            {
                var newVote = new ForumThreadVote
                {
                    UserId = userId,
                    ForumThreadId = threadId,
                    IsUpvote = isUpVote,
                    VotedAt = DateTime.UtcNow
                };

                await _forumThreadVoteRepository.AddForumThreadVote(newVote);
            }
        }

        public async Task<int> GetVoteThreadCount (int threadId)
        {
            var countVote = await _forumThreadVoteRepository.GetCountVoteForumThread(threadId);
            return countVote <= 0 ? 0 : countVote;
        }
    }
}
