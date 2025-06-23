using ForumWebsite.Dtos.Tags;
using ForumWebsite.Models;

namespace ForumWebsite.Services.Tags
{
    public interface ITagService
    {
        Task<ICollection<TagDto>> GetAllTagsAsync();
        Task<TagDto?> GetTagByIdAsync(int tagId);
        Task CreateTagAsync(CreateTagDto tagDto);
        Task UpdateTagAsync(UpdateTagDto tagDto, int tagId);
        Task DeleteTagAsync(int tagId);
    }
}