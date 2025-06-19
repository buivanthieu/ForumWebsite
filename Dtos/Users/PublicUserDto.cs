using ForumWebsite.Models;

namespace ForumWebsite.Dtos.Users
{
    public class PublicUserDto
    {
        public string Role { get; set; } = null!;
        public string DisplayName { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public string AvatarUrl { get; set; } = null!;
        public string Bio { get; set; } = null!;
        public string Location { get; set; } = null!;
        public int Reputation { get; set; }
        public int TotalThreads { get; set; }
        public int TotalComments { get; set; }
        public ICollection<ForumThread>? ForumThreads { get; set; }
        public ICollection<Comment>? Comments { get; set; }
        public ICollection<CommentVote>? CommentVotes { get; set; }
        public ICollection<ForumThreadVote>? ForumThreadVotes { get; set; }
    }
}
