using ForumWebsite.Models;

namespace ForumWebsite.Dtos.ForumThreads
{
    public class ForumThreadDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int? TopicId { get; set; }
        public int? UserId { get; set; }
        
    }
}
