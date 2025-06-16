namespace ForumWebsite.Dtos.Comments
{
    public class CreateCommentDto
    {
        public string Content { get; set; } = null!;
        public int ThreadId { get; set; }  
        public int? ParentCommentId { get; set; }
    }
}
