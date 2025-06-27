using AutoMapper;
using ForumWebsite.Dtos.Comments;
using ForumWebsite.Dtos.ForumThreads;
using ForumWebsite.Models;
using ForumWebsite.Repositories.Comments;
using ForumWebsite.Repositories.ForumThreads;
using ForumWebsite.Repositories.ThreadTags;
using ForumWebsite.Repositories.Users;
using ForumWebsite.Services.Votes;
using System.Threading;

namespace ForumWebsite.Services.ForumThreads
{
    public class ForumThreadService : IForumThreadService
    {
        private readonly IForumThreadRepository _forumThreadRepository;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IThreadTagRepository _threadTagRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly IForumThreadVoteService _forumThreadVoteService;
        public ForumThreadService(IForumThreadRepository forumThreadRepository, IMapper mapper, 
            IUserRepository userRepository, IThreadTagRepository threadTagRepository,
            ICommentRepository commentRepository, IForumThreadVoteService forumThreadVoteService)
        {
            _forumThreadRepository = forumThreadRepository;
            _mapper = mapper;
            _userRepository = userRepository;
            _threadTagRepository = threadTagRepository;
            _commentRepository = commentRepository;
            _forumThreadVoteService = forumThreadVoteService;
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

        

        public async Task<ForumThreadDetailDto> GetThreadById(int threadId)
        {
            var thread = await _forumThreadRepository.GetForumThreadById(threadId);
            var comments = await _commentRepository.GetCommentsByThreadId(threadId);
            var dto = _mapper.Map<ForumThreadDetailDto>(thread);
            dto.DisplayName = thread.User?.DisplayName ?? "Unknown";
            dto.TopicName = thread.Topic?.Name ?? "Unknown";
            dto.TagIds = thread.ThreadTags?
               .Select(tt => tt.TagId)
               .ToList();

            dto.TagNames = thread.ThreadTags?
                .Select(tt => tt.Tag?.Name ?? "")
                .ToList();
            dto.Comments = _mapper.Map<ICollection<CommentDto>>(comments);

            return dto;
        }

        public async Task<ICollection<ForumThreadDto>> GetThreads()
        {
            var threads = await _forumThreadRepository.GetAllForumThreads();
            var resultTasks = threads.Select(async thread => new ForumThreadDto
            {
                Id = thread.Id,
                Title = thread.Title,
                Content = thread.Content,
                CreatedAt = thread.CreatedAt,
                TopicId = thread.TopicId,
                TopicName = thread.Topic?.Name ?? "Unknown",
                UserId = thread.UserId ?? 0,
                DisplayName = thread.User?.DisplayName ?? "Unknown",
                TagIds = thread.ThreadTags?
                    .Select(tt => tt.TagId)
                    .ToList(),
                TagNames = thread.ThreadTags?
                    .Select(tt => tt.Tag?.Name ?? "")
                    .ToList(),
                ForumThreadVoteDto = await _forumThreadVoteService.GetForumThreadVote(thread.Id),
            });

            var result = await Task.WhenAll(resultTasks);
            return result.ToList();
        }


        public async Task<ICollection<ForumThreadDto>> SearchThreadByName(string name)
        {
            var threads = await _forumThreadRepository.SearchThreadByName(name);
            var result = threads.Select(thread => new ForumThreadDto
            {
                Id = thread.Id,
                Title = thread.Title,
                Content = thread.Content,
                CreatedAt = thread.CreatedAt,
                TopicId = thread.TopicId,
                TopicName = thread.Topic?.Name ?? "Unknown",
                UserId = thread.UserId ?? 0,
                DisplayName = thread.User?.DisplayName ?? "Unknown",
                TagIds = thread.ThreadTags?
                    .Select(tt => tt.TagId)
                    .ToList(),
                TagNames = thread.ThreadTags?
                    .Select(tt => tt.Tag?.Name ?? "")
                    .ToList()
            }).ToList();
            return result;
        }
        public async Task<ICollection<ForumThreadDto>> FillterThreadByTopicAndTag(int? topicId, List<int> tagIds)
        {
            var threads = await _forumThreadRepository.GetForumThreadByTopicAndTag(topicId, tagIds);
            var result = threads.Select(thread => new ForumThreadDto
            {
                Id = thread.Id,
                Title = thread.Title,
                Content = thread.Content,
                CreatedAt = thread.CreatedAt,
                TopicId = thread.TopicId,
                TopicName = thread.Topic?.Name ?? "Unknown",
                UserId = thread.UserId ?? 0,
                DisplayName = thread.User?.DisplayName ?? "Unknown",
                TagIds = thread.ThreadTags?
                    .Select(tt => tt.TagId)
                    .ToList(),
                TagNames = thread.ThreadTags?
                    .Select(tt => tt.Tag?.Name ?? "")
                    .ToList()
            }).ToList();
            return result;
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
