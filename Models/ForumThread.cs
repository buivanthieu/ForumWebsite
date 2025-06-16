namespace ForumWebsite.Models
{
    public class ForumThread
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int TopicId { get; set; }
        public Topic? Topic { get; set; }
        public int? UserId { get; set; }
        public User? User { get; set; }

        public ICollection<Comment>? Comments { get; set; }
        public ICollection<ForumThreadVote>? Votes { get; set; }
    }

}
