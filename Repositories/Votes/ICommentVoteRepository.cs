using ForumWebsite.Models;

namespace ForumWebsite.Repositories.Votes
{
    public interface ICommentVoteRepository
    {
        Task<CommentVote> GetCommentVote(int userId, int commentId);
        Task AddCommentVote(CommentVote vote);
        Task UpdateCommentVote(CommentVote vote);
        Task DeleteCommentVote(CommentVote vote);
        Task<ICollection<User>> GetUserVoteUpComment(int commentId);
        Task<ICollection<User>> GetUserVoteDownComment(int commentId);
        Task UpdateCommentReputation(int commentId);
        Task<int?> GetCommentOwnerId(int commentId);

    }
}
