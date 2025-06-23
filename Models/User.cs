namespace ForumWebsite.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string Role { get; set; } = "User"; // User, Admin, Mod
        public string DisplayName { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public string AvatarUrl { get; set; } = null!;
        public string? Bio { get; set; }
        public string? Location { get; set; }
        public DateTime JoinedAt { get; set; } = DateTime.UtcNow;
        public bool IsBanned { get; set; }

        public int Reputation { get; set; } 
        public int TotalThreads { get; set; }
        public int TotalComments { get; set; }

        public ICollection<ForumThread> ForumThreads { get; set; } = new List<ForumThread>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<CommentVote> CommentVotes { get; set; } = new List<CommentVote>();
        public ICollection<ForumThreadVote> ForumThreadVotes { get; set; } = new List<ForumThreadVote>();
    }

}
