namespace ForumWebsite.Dtos.Users
{
    public class UserBaseDto
    {
        public string Role { get; set; } = null!;
        public string DisplayName { get; set; } = null!;
        public string AvatarUrl { get; set; } = null!;
    }
}
