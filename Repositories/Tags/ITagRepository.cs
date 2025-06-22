using ForumWebsite.Models;

namespace ForumWebsite.Repositories.Tags
{
    public interface ITagRepository
    {
        Task<ICollection<Tag>> GetAllTags();
        Task<Tag?> GetTagById(int tagId);
        Task CreateTag(Tag tag);
        Task UpdateTag(Tag tag, int tagId);
        Task DeleteTag(int tagId);
    }
}