using ForumWebsite.Models;

namespace ForumWebsite.Repositories.Topics
{
    public interface ITopicRepository
    {
        Task<Topic> GetTopicById(int topicId);
        Task<ICollection<Topic>> GetAllTopic();
        Task CreateTopic(Topic topic);
        Task DeleteTopic(int topicId);
        Task UpdateTopic(Topic topic, int topicId);
    }
}