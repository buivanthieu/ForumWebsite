namespace ForumWebsite.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;

        public ICollection<ThreadTag>? ThreadTags { get; set; }
    }
}
