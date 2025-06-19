using ForumWebsite.Models;

namespace ForumWebsite.Repositories.ForumThreads
{
    public interface IForumThreadRepository
    {
        Task<ICollection<ForumThread>> GetAllForumThreads();
        Task<ForumThread> GetForumThreadById(int id);
        Task<ICollection<ForumThread>> SearchThreadByName(string name);
        Task AddForumThread(ForumThread forumThread);
        Task DeleteForumThread(int id);
        Task UpdateForumThread(ForumThread forumThread);
    }
}
