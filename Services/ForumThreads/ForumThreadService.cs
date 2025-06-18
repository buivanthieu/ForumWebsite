using AutoMapper;
using ForumWebsite.Dtos.ForumThreads;
using ForumWebsite.Models;
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

        public async Task<ForumThreadDto> CreateThread(CreateForumThreadDto dto, int userId)
        {
            var forumThread = _mapper.Map<ForumThread>(dto);
            forumThread.UserId = userId;
            forumThread.CreatedAt = DateTime.Now;

            await _forumThreadRepository.AddForumThread(forumThread);
            return _mapper.Map<ForumThreadDto>(forumThread);

        }

        public async Task DeleteThread(int threadId, int userId)
        {
            var thread = await _forumThreadRepository.GetForumThreadById(threadId);
            if(thread.UserId == userId)
            {
                throw new UnauthorizedAccessException("You are not authorized to delete this thread.");
            }
            await _forumThreadRepository.DeleteForumThread(threadId);

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

        public async Task UpdateThread(int threadId, UpdateForumThreadDto updateForumThreadDto, int userId)
        {
            var thread = await _forumThreadRepository.GetForumThreadById(threadId);
            if (thread.UserId != userId)
            {
                throw new UnauthorizedAccessException("You are not authorized to edit this thread.");
            }
            thread.Content = updateForumThreadDto.Content;
            await _forumThreadRepository.UpdateForumThread(thread);
        }
    }
}
