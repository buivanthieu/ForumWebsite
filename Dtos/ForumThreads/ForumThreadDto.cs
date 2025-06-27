
using ForumWebsite.Dtos.Votes;

namespace ForumWebsite.Dtos.ForumThreads
{
    public class ForumThreadDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int TopicId { get; set; }
        public string TopicName { get; set; } = null!;
        public int UserId { get; set; }
        public string DisplayName { get; set; } = null!;
        public ICollection<int>? TagIds { get; set; }
        public ICollection<string>? TagNames { get; set; }
        public ForumThreadVoteDto? ForumThreadVoteDto { get; internal set; }
    }
}
