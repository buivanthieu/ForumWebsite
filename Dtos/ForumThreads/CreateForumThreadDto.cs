using ForumWebsite.Models;

namespace ForumWebsite.Dtos.ForumThreads
{
    public class CreateForumThreadDto
    {
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public int? TopicId { get; set; }
        public List<int>? TagIds { get; set; }
    }
}
