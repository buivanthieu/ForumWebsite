using ForumWebsite.Dtos.Comments;

namespace ForumWebsite.Services.Comments
{
    public interface ICommentService
    {
        Task<ICollection<CommentDto>> GetCommentsByThreadId(int threadId);
        Task<CommentDto> GetCommentById(int commentId);
        Task<CommentDto> CreateComment(CreateCommentDto dto, int userId);
        Task UpdateComment(int commentId, UpdateCommentDto dto, int userId);
        Task DeleteComment(int commentId, int userId);
        Task<(int upvotes, int downvotes)> GetVoteSummary(int commentId);
    }
}
