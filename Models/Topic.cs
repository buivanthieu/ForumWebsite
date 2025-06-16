namespace ForumWebsite.Models
{
    public class Topic
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public ICollection<ForumThread>? ForumThreads { get; set; }
    }
}
