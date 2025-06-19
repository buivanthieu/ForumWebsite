using ForumWebsite.Models;

namespace ForumWebsite.Dtos.Users
{
    public class UpdateCurrentUserDto
    {
        public string DisplayName { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public string AvatarUrl { get; set; } = null!;
        public string Bio { get; set; } = null!;
        public string Location { get; set; } = null!;
    }
}
