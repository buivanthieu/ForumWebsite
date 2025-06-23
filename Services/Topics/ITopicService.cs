using ForumWebsite.Dtos.Topics;
using ForumWebsite.Models;

namespace ForumWebsite.Services.Topics
{
    public interface ITopicService
    {
        Task<ICollection<TopicDto>> GetAllTopicsAsync();
        Task<TopicDto?> GetTopicByIdAsync(int topicId);
        Task CreateTopicAsync(CreateTopicDto topicDto);
        Task UpdateTopicAsync(UpdateTopicDto topicDto, int topicId);
        Task DeleteTopicAsync(int topicId);
    }
}