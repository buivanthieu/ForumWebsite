namespace ForumWebsite.Models
{
    public class ThreadTag
    {
        public int ForumThreadId { get; set; }
        public ForumThread ForumThread { get; set; } = null!;

        public int TagId { get; set; }
        public Tag Tag { get; set; } = null!;
    }
}
