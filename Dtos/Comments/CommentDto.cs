namespace ForumWebsite.Dtos.Comments
{
    public class CommentDto
    {
        public int Id { get; set; }
        public string Content { get; set; } = null!;
        public DateTime CreatedAt { get; set; }

        public int? UserId { get; set; }
        public string? UserName { get; set; } 

        public int? ThreadId { get; set; }

        public int? ParentCommentId { get; set; }

        public List<CommentDto>? Replies { get; set; }

        public int Upvotes { get; set; }
        public int Downvotes { get; set; }
    }
}
