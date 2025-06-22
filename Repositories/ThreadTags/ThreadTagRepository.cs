using ForumWebsite.Datas;
using ForumWebsite.Models;
using Microsoft.EntityFrameworkCore;

namespace ForumWebsite.Repositories.ThreadTags
{
    public class ThreadTagRepository : IThreadTagRepository
    {
        private readonly ApplicationDbContext _context;

        public ThreadTagRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<ThreadTag>> GetAllThreadTag()
        {
            return await _context.ThreadTags.ToListAsync();
        }

        public async Task<ICollection<ThreadTag>> GetThreadTagByThreadId(int threadId)
        {
            return await _context.ThreadTags
                .Where(tt => tt.ForumThreadId == threadId)
                .Include(tt => tt.Tag)
                .ToListAsync();
        }

        public async Task<ICollection<ThreadTag>> GetThreadTagByTagId(int tagId)
        {
            return await _context.ThreadTags
                .Where(tt => tt.TagId == tagId)
                .Include(tt => tt.ForumThread)
                .ToListAsync();
        }

        public async Task<ThreadTag> GetThreadTagByThreadAndTagId(int threadId, int tagId)
        {
            var threadTag = await _context.ThreadTags
                .FirstOrDefaultAsync(tt => tt.ForumThreadId == threadId && tt.TagId == tagId)
                ?? throw new KeyNotFoundException("key null");
            return threadTag;
        }

        public async Task DeleteThreadTag(int threadId, int tagId)
        {
            var entity = await GetThreadTagByThreadAndTagId(threadId, tagId);
            if (entity != null)
            {
                _context.ThreadTags.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task CreateThreadTag(ThreadTag threadTag)
        {
            _context.ThreadTags.Add(threadTag);
            await _context.SaveChangesAsync();
        }

        public async Task CreateThreadTagWithManyTagId(int threadId, List<int> tagIds)
        {
            var newTags = tagIds.Select(tagId => new ThreadTag
            {
                ForumThreadId = threadId,
                TagId = tagId
            });

            _context.ThreadTags.AddRange(newTags);
            await _context.SaveChangesAsync();
        }
    }
}
