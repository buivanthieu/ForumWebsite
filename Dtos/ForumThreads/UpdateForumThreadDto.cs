namespace ForumWebsite.Dtos.ForumThreads
{
    public class UpdateForumThreadDto
    {
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public int? TopicId { get; set; }
    }
}
