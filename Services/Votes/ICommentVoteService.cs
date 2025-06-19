namespace ForumWebsite.Services.Votes
{
    public interface ICommentVoteService
    {
        Task VoteComment(int userId, int commentId, bool isUpVote);
        Task<int> GetVoteCommentCount(int commentId);
    }
}
