using AutoMapper;
using ForumWebsite.Dtos.ForumThreads;
using ForumWebsite.Repositories.ForumThreads;

namespace ForumWebsite.Services.ForumThreads
{
    public class ForumThreadService : IForumThreadService
    {
        private readonly IForumThreadRepository _forumThreadRepository;
        private readonly IMapper _mapper;
        public ForumThreadService(IForumThreadRepository forumThreadRepository, IMapper mapper)
        {
            _forumThreadRepository = forumThreadRepository;
            _mapper = mapper;
        }

        public Task<ForumThreadDto> CreateThread(CreateForumThreadDto dto, int userId)
        {
            throw new NotImplementedException();
        }

        public Task DeleteThread(int threadId, int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<ForumThreadDto> GetThreadById(int threadId)
        {
            var thread = await _forumThreadRepository.GetForumThreadById(threadId);
            return _mapper.Map<ForumThreadDto>(thread);
        }

        public async Task<ICollection<ForumThreadDto>> GetThreads()
        {
            var threads = await _forumThreadRepository.GetAllForumThreads();
            return _mapper.Map<ICollection<ForumThreadDto>>(threads);
        }

        public async Task<ICollection<ForumThreadDto>> SearchThreadByName(string name)
        {
            var threads = await _forumThreadRepository.SearchThreadByName(name);
            return _mapper.Map<ICollection<ForumThreadDto>>(threads);
        }

        public Task UpdateThread(int threadId, UpdateForumThreadDto dto, int userId)
        {
            throw new NotImplementedException();
        }
    }
}
