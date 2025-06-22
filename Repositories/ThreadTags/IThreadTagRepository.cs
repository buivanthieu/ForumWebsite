using ForumWebsite.Models;

namespace ForumWebsite.Repositories.ThreadTags
{
    public interface IThreadTagRepository
    {
        Task<ICollection<ThreadTag>> GetAllThreadTag();
        Task<ICollection<ThreadTag>> GetThreadTagByThreadId(int threadId);
        Task<ICollection<ThreadTag>> GetThreadTagByTagId(int tagId);
        Task<ThreadTag> GetThreadTagByThreadAndTagId(int threadId, int tagId);
        Task DeleteThreadTag(int threadId, int tagId);
        Task CreateThreadTag(ThreadTag threadTag);
        Task CreateThreadTagWithManyTagId(int threadId, List<int> tagIds);

    }
}