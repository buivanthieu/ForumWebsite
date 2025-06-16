namespace ForumWebsite.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string Role { get; set; } = "User"; // User, Moderator, Admin

        public ICollection<ForumThread>? ForumThreads { get; set; }
        public ICollection<Comment>? Comments { get; set; }
        public ICollection<CommentVote>? CommentVotes { get; set; }
        public ICollection<ForumThreadVote>? ForumThreadVotes { get; set; }

    }
}
