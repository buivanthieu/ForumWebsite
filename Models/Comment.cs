namespace ForumWebsite.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int? UserId { get; set; }
        public User? User { get; set; }

        public int? ThreadId { get; set; }
        public ForumThread? ForumThread { get; set; }

        public int? ParentCommentId { get; set; }
        public Comment? ParentComment { get; set; }

        public ICollection<Comment>? Replies { get; set; }
        public ICollection<CommentVote>? Votes { get; set; }
    }

}
