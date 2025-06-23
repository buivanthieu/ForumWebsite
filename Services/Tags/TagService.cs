using AutoMapper;
using ForumWebsite.Dtos.Tags;
using ForumWebsite.Models;
using ForumWebsite.Repositories.Tags;

namespace ForumWebsite.Services.Tags
{
    public class TagService : ITagService
    {
        private readonly ITagRepository _tagRepository;
        private readonly IMapper _mapper;
        public TagService(ITagRepository tagRepository, IMapper mapper)
        {
            _tagRepository = tagRepository;
            _mapper = mapper;
        }

        public async Task<ICollection<TagDto>> GetAllTagsAsync()
        {
            var tags = await _tagRepository.GetAllTags();
            return _mapper.Map<ICollection<TagDto>>(tags);
        }

        public async Task<TagDto?> GetTagByIdAsync(int tagId)
        {
            var tag = await _tagRepository.GetTagById(tagId);
            return _mapper?.Map<TagDto>(tag);
        }

        public async Task CreateTagAsync(CreateTagDto tagDto)
        {
            var tag = _mapper.Map<Tag>(tagDto);
            await _tagRepository.CreateTag(tag);
        }

        public async Task UpdateTagAsync(UpdateTagDto tagDto, int tagId)
        {
            var tag = _mapper.Map<Tag>(tagDto);
            await _tagRepository.UpdateTag(tag, tagId);
        }

        public async Task DeleteTagAsync(int tagId)
        {
            await _tagRepository.DeleteTag(tagId);
        }
    }
}
