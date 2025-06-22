using ForumWebsite.Models;

namespace ForumWebsite.Services.Tags
{
    public interface ITagService
    {
        Task<ICollection<Tag>> GetAllTagsAsync();
        Task<Tag?> GetTagByIdAsync(int tagId);
        Task CreateTagAsync(Tag tag);
        Task UpdateTagAsync(Tag tag, int tagId);
        Task DeleteTagAsync(int tagId);
    }
}