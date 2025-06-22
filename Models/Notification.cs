namespace ForumWebsite.Models
{
    public class Notification
    {
        public int Id { get; set; }

        public int ReceiverId { get; set; }
        public User Receiver { get; set; } = null!;

        public string Message { get; set; } = null!;
        public string? Link { get; set; }

        public bool? IsRead { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
