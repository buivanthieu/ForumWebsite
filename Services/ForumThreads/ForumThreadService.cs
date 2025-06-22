using AutoMapper;
using ForumWebsite.Dtos.ForumThreads;
using ForumWebsite.Models;
using ForumWebsite.Repositories.ForumThreads;
using ForumWebsite.Repositories.ThreadTags;
using ForumWebsite.Repositories.Users;
using System.Threading;

namespace ForumWebsite.Services.ForumThreads
{
    public class ForumThreadService : IForumThreadService
    {
        private readonly IForumThreadRepository _forumThreadRepository;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IThreadTagRepository _threadTagRepository;
        public ForumThreadService(IForumThreadRepository forumThreadRepository, IMapper mapper, 
            IUserRepository userRepository, IThreadTagRepository threadTagRepository)
        {
            _forumThreadRepository = forumThreadRepository;
            _mapper = mapper;
            _userRepository = userRepository;
            _threadTagRepository = threadTagRepository;
        }

        public async Task<ForumThreadDto> CreateThread(CreateForumThreadDto dto, int userId)
        {
            var forumThread = _mapper.Map<ForumThread>(dto);
            forumThread.UserId = userId;
            forumThread.CreatedAt = DateTime.Now;

            await _forumThreadRepository.AddForumThread(forumThread);


            var user = await _userRepository.GetUserById(userId);
            user.TotalThreads += 1;
            await _userRepository.UpdateUser(user);

            if (dto.TagIds is not null && dto.TagIds.Any())
            {
                foreach (var tagId in dto.TagIds)
                {
                    var threadTag = new ThreadTag
                    {
                        ForumThreadId = forumThread.Id,
                        TagId = tagId
                    };
                    await _threadTagRepository.CreateThreadTag(threadTag);
                }
            }
            return _mapper.Map<ForumThreadDto>(forumThread);

        }

        public async Task DeleteThread(int threadId, int userId)
        {
            var thread = await _forumThreadRepository.GetForumThreadById(threadId);
            if(thread.UserId != userId)
            {
                throw new UnauthorizedAccessException("You are not authorized to delete this thread.");
            }
            await _forumThreadRepository.DeleteForumThread(threadId);

            var user = await _userRepository.GetUserById(userId);
            user.TotalThreads -= 1;
            await _userRepository.UpdateUser(user);
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
            thread.Title = updateForumThreadDto.Title;
            thread.Content = updateForumThreadDto.Content;
            thread.TopicId = updateForumThreadDto.TopicId ?? thread.TopicId;
            await _forumThreadRepository.UpdateForumThread(thread);
        }
    }
}
