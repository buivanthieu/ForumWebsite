using ForumWebsite.Dtos.Comments;
using ForumWebsite.Dtos.ForumThreads;
using ForumWebsite.Dtos.Votes;
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
        public ICollection<ForumThreadDto>? ForumThreads { get; set; }
        public ICollection<CommentDto>? Comments { get; set; }
        //public ICollection<CommentVoteDto>? CommentVotes { get; set; }
        //public ICollection<ForumThreadVoteDto>? ForumThreadVotes { get; set; }
    }
}
