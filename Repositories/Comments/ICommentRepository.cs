using ForumWebsite.Models;

namespace ForumWebsite.Repositories.Comments
{
    public interface ICommentRepository
    {
        Task<ICollection<Comment>> GetAllComments();
        Task<Comment> GetCommentById(int id);
        Task<ICollection<Comment>> GetCommentsByThreadId(int threadId);
        Task AddComment(Comment comment);
        Task DeleteComment(int id);
        Task UpdateComment(Comment comment);
    }
}
