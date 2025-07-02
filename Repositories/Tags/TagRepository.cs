using ForumWebsite.Datas;
using ForumWebsite.Models;
using Microsoft.EntityFrameworkCore;

namespace ForumWebsite.Repositories.Tags
{
    public class TagRepository : ITagRepository
    {
        private readonly ApplicationDbContext _context;

        public TagRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Tag>> GetAllTags()
        {
            return await _context.Tags.ToListAsync();
        }

        public async Task<Tag?> GetTagById(int tagId)
        {
            var tag =  await _context.Tags.FindAsync(tagId)
                ?? throw new KeyNotFoundException("key is null");
            return tag;        
        }

        public async Task CreateTag(Tag tag)
        {
            _context.Tags.Add(tag);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTag(Tag tag, int tagId)
        {
            var existingTag = await _context.Tags.FindAsync(tagId);
            if (existingTag != null)
            {
                existingTag.Name = tag.Name;
                existingTag.Description = tag.Description;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteTag(int tagId)
        {
            var tag = await _context.Tags.FindAsync(tagId);
            if (tag != null)
            {
                _context.Tags.Remove(tag);
                await _context.SaveChangesAsync();
            }
        }
    }
}
