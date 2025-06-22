using ForumWebsite.Models;

namespace ForumWebsite.Repositories.Votes
{
    public interface IForumThreadVoteRepository
    {
        Task<ForumThreadVote> GetForumThreadVote(int userId, int forumThreadId);
        Task AddForumThreadVote(ForumThreadVote vote);
        Task UpdateForumThreadVote(ForumThreadVote vote);
        Task DeleteForumThreadVote(ForumThreadVote vote);
        Task<int> GetCountUpVoteForumThread(int forumThreadId);
        Task<int> GetCountDownVoteForumThread(int forumThreadId);
        Task<ICollection<User>> GetUserVoteUpThread(int forumThreadId);
        Task<ICollection<User>> GetUserVoteDownThread(int forumThreadId);

        Task UpdateForumThreadReputation(int threadId);
        Task<int?> GetForumThreadOwnerId(int threadId);


    }
}
