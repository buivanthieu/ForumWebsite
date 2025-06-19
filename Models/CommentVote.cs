namespace ForumWebsite.Models
{
    public class CommentVote
    {
        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public int CommentId { get; set; }
        public Comment Comment { get; set; } = null!;

        public bool IsUpvote { get; set; }  // true = upvote, false = downvote
        public DateTime VotedAt { get; set; } = DateTime.UtcNow;
    }
}
