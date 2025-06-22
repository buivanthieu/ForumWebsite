using ForumWebsite.Models;

namespace ForumWebsite.Services.Topics
{
    public interface ITopicService
    {
        Task<ICollection<Topic>> GetAllTopicsAsync();
        Task<Topic?> GetTopicByIdAsync(int topicId);
        Task CreateTopicAsync(Topic topic);
        Task UpdateTopicAsync(Topic topic, int topicId);
        Task DeleteTopicAsync(int topicId);
    }
}