using ForumWebsite.Datas;
using ForumWebsite.Models;
using Microsoft.EntityFrameworkCore;

namespace ForumWebsite.Repositories.Topics
{
    public class TopicRepository : ITopicRepository
    {
        private readonly ApplicationDbContext _context;

        public TopicRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Topic>> GetAllTopic()
        {
            return await _context.Topics.ToListAsync();
        }

        public async Task<Topic> GetTopicById(int topicId)
        {
            var topic = await _context.Topics.FindAsync(topicId)
                ?? throw new KeyNotFoundException("key is null");
            return topic;
        }

        public async Task CreateTopic(Topic topic)
        {
            _context.Topics.Add(topic);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTopic(int topicId)
        {
            var topic = await _context.Topics.FindAsync(topicId);
            if (topic != null)
            {
                _context.Topics.Remove(topic);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateTopic(Topic topic, int topicId)
        {
            var existing = await _context.Topics.FindAsync(topicId);
            if (existing != null)
            {
                existing.Name = topic.Name;
                existing.Description = topic.Description;
                await _context.SaveChangesAsync();
            }
        }
    }
}
