namespace ForumWebsite.Services.Votes
{
    public interface IForumThreadVoteService
    {
        Task VoteThread (int userId, int threadId, bool isUpVote);
        Task<int> GetVoteThreadCount (int threadId);

    }
}
