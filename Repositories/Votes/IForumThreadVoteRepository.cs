using ForumWebsite.Models;

namespace ForumWebsite.Repositories.Votes
{
    public interface IForumThreadVoteRepository
    {
        Task<ForumThreadVote> GetForumThreadVote(int userId, int commentId);
        Task AddForumThreadVote(ForumThreadVote vote);
        Task UpdateForumThreadVote(ForumThreadVote vote);
        Task DeleteForumThreadVote(ForumThreadVote vote);
    }
}
