using AutoMapper;
using ForumWebsite.Dtos.Users;
using ForumWebsite.Dtos.Votes;
using ForumWebsite.Models;
using ForumWebsite.Repositories.ForumThreads;
using ForumWebsite.Repositories.Users;
using ForumWebsite.Repositories.Votes;

namespace ForumWebsite.Services.Votes
{
    public class ForumThreadVoteService : IForumThreadVoteService
    {
        private readonly IForumThreadVoteRepository _forumThreadVoteRepository;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IForumThreadRepository _forumThreadRepository;

        public ForumThreadVoteService (IForumThreadVoteRepository forumThreadVoteRepository, IMapper mapper, 
            IUserRepository userRepository, IForumThreadRepository forumThreadRepository)
        {
            _forumThreadVoteRepository = forumThreadVoteRepository;
            _mapper = mapper;
            _userRepository = userRepository;
            _forumThreadRepository = forumThreadRepository;
        }

        public async Task VoteThread(int userId, int threadId, bool isUpVote)
        {
            var existingForumThreadVote = await _forumThreadVoteRepository.GetForumThreadVote(userId, threadId);
            
            if (existingForumThreadVote != null)
            {
                if (existingForumThreadVote.IsUpvote == isUpVote)
                {
                    await _forumThreadVoteRepository.DeleteForumThreadVote(existingForumThreadVote);
                }
                else
                {
                    existingForumThreadVote.IsUpvote = isUpVote;
                    existingForumThreadVote.VotedAt = DateTime.UtcNow;
                    await _forumThreadVoteRepository.UpdateForumThreadVote(existingForumThreadVote);
                }
            }
            else
            {
                var newVote = new ForumThreadVote
                {
                    UserId = userId,
                    ForumThreadId = threadId,
                    IsUpvote = isUpVote,
                    VotedAt = DateTime.UtcNow
                };

                await _forumThreadVoteRepository.AddForumThreadVote(newVote);
            }

            await _forumThreadVoteRepository.UpdateForumThreadReputation(threadId);
            var ownerId = await _forumThreadVoteRepository.GetForumThreadOwnerId(threadId);
            if (ownerId.HasValue)
            {
                await _userRepository.UpdateUserReputation(ownerId.Value);
            }
        }

        public async Task<int> GetUpVoteThreadCount (int threadId)
        {
            var countVote = await _forumThreadVoteRepository.GetCountUpVoteForumThread(threadId);
            return countVote;
        }
        public async Task<int> GetDownVoteThreadCount (int threadId)
        {
            var countVote = await _forumThreadVoteRepository.GetCountDownVoteForumThread(threadId);
            return countVote;
        }
        public async Task<ICollection<PublicUserDto>> GetUserVoteUpThread(int threadId)
        {
            var userVoteUp = await _forumThreadVoteRepository.GetUserVoteUpThread(threadId);
            return  _mapper.Map<ICollection<PublicUserDto>>(userVoteUp);
        }
        public async Task<ICollection<PublicUserDto>> GetUserVoteDownThread(int threadId)
        {
            var userVoteDown =await _forumThreadVoteRepository.GetUserVoteDownThread(threadId);
            return  _mapper.Map<ICollection<PublicUserDto>>(userVoteDown);
        }

        public async Task<ForumThreadVoteDto> GetForumThreadVote(int threadId)
        {
            var forumThread = await _forumThreadRepository.GetForumThreadById(threadId);
            var userVoteUp = await _forumThreadVoteRepository.GetUserVoteUpThread(threadId);
            var userVoteDown = await _forumThreadVoteRepository.GetUserVoteDownThread (threadId);
            var upDtos = _mapper.Map<ICollection<UserBaseDto>>(userVoteUp);
            var downDtos = _mapper.Map<ICollection<UserBaseDto>> (userVoteDown);
            return new ForumThreadVoteDto
            {
                CountUpVote = upDtos.Count,
                CountDownVote = downDtos.Count,
                Reputation = forumThread.Reputation,
                UsersVoteUp = upDtos,
                UsersVoteDown = downDtos

            };
        }
    }
}
