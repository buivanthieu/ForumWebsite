using ForumWebsite.Models;

namespace ForumWebsite.Repositories.Votes
{
    public interface ICommentVoteRepository
    {
        Task<CommentVote> GetCommentVote(int userId, int commentId);
        Task AddCommentVote(CommentVote vote);
        Task UpdateCommentVote(CommentVote vote);
        Task DeleteCommentVote(CommentVote vote);
        Task<int> GetCommentVoteCount(int commentId);
        //Task<int> GetCommentVoteCount(int userId);
    }
}
