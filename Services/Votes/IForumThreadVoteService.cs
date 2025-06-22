using ForumWebsite.Dtos.Users;
using ForumWebsite.Dtos.Votes;

namespace ForumWebsite.Services.Votes
{
    public interface IForumThreadVoteService
    {
        Task VoteThread (int userId, int threadId, bool isUpVote);
        Task<int> GetUpVoteThreadCount (int threadId);
        Task<ICollection<PublicUserDto>> GetUserVoteUpThread(int threadId);
        Task<int> GetDownVoteThreadCount(int threadId);
        Task<ICollection<PublicUserDto>> GetUserVoteDownThread(int threadId);
        Task<ForumThreadVoteDto> GetForumThreadVote(int threadId);

    }
}
