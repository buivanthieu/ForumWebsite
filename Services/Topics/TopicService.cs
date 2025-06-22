using ForumWebsite.Models;
using ForumWebsite.Repositories.Topics;

namespace ForumWebsite.Services.Topics
{
    public class TopicService : ITopicService
    {
        private readonly ITopicRepository _topicRepository;

        public TopicService(ITopicRepository topicRepository)
        {
            _topicRepository = topicRepository;
        }

        public async Task<ICollection<Topic>> GetAllTopicsAsync()
        {
            return await _topicRepository.GetAllTopic();
        }

        public async Task<Topic?> GetTopicByIdAsync(int topicId)
        {
            return await _topicRepository.GetTopicById(topicId);
        }

        public async Task CreateTopicAsync(Topic topic)
        {
            await _topicRepository.CreateTopic(topic);
        }

        public async Task UpdateTopicAsync(Topic topic, int topicId)
        {
            await _topicRepository.UpdateTopic(topic, topicId);
        }

        public async Task DeleteTopicAsync(int topicId)
        {
            await _topicRepository.DeleteTopic(topicId);
        }
    }
}
