using ForumWebsite.Models;
using ForumWebsite.Repositories.Tags;

namespace ForumWebsite.Services.Tags
{
    public class TagService : ITagService
    {
        private readonly ITagRepository _tagRepository;

        public TagService(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        public async Task<ICollection<Tag>> GetAllTagsAsync()
        {
            return await _tagRepository.GetAllTags();
        }

        public async Task<Tag?> GetTagByIdAsync(int tagId)
        {
            return await _tagRepository.GetTagById(tagId);
        }

        public async Task CreateTagAsync(Tag tag)
        {
            await _tagRepository.CreateTag(tag);
        }

        public async Task UpdateTagAsync(Tag tag, int tagId)
        {
            await _tagRepository.UpdateTag(tag, tagId);
        }

        public async Task DeleteTagAsync(int tagId)
        {
            await _tagRepository.DeleteTag(tagId);
        }
    }
}
