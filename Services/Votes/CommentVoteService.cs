using ForumWebsite.Models;
using ForumWebsite.Repositories.Votes;

namespace ForumWebsite.Services.Votes
{
    public class CommentVoteService : ICommentVoteService
    {
        private readonly ICommentVoteRepository _commentVoteRepository;
        public CommentVoteService(ICommentVoteRepository commentVoteRepository)
        {
            _commentVoteRepository = commentVoteRepository;
        }

        public async Task<int> GetVoteCommentCount(int commentId)
        {
            var countVote = await _commentVoteRepository.GetCommentVoteCount(commentId);
            return countVote;
        }

        public async Task VoteComment(int userId, int commentId, bool isUpVote)
        {
            var existingCommentVote = await _commentVoteRepository.GetCommentVote(userId, commentId);
            if (existingCommentVote != null)
            {
                if (existingCommentVote.IsUpvote == isUpVote)
                {
                    await _commentVoteRepository.DeleteCommentVote(existingCommentVote);
                }
                else
                {
                    existingCommentVote.IsUpvote = isUpVote;
                    existingCommentVote.VotedAt = DateTime.UtcNow;
                    await _commentVoteRepository.UpdateCommentVote(existingCommentVote);
                }
            }
            else
            {
                var newVote = new CommentVote
                {
                    UserId = userId,
                    CommentId = commentId,
                    IsUpvote = isUpVote,
                    VotedAt = DateTime.UtcNow
                };
                await _commentVoteRepository.AddCommentVote(newVote);
            }
        }
    }
}
