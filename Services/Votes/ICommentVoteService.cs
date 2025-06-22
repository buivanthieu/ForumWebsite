using ForumWebsite.Dtos.Votes;

namespace ForumWebsite.Services.Votes
{
    public interface ICommentVoteService
    {
        Task VoteComment(int userId, int commentId, bool isUpVote);
        Task<CommentVoteDto> GetCommentVote(int commentId);
    }
}
