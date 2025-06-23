using AutoMapper;
using ForumWebsite.Dtos.Topics;
using ForumWebsite.Models;
using ForumWebsite.Repositories.Topics;

namespace ForumWebsite.Services.Topics
{
    public class TopicService : ITopicService
    {
        private readonly ITopicRepository _topicRepository;
        private readonly IMapper _mapper;

        public TopicService(ITopicRepository topicRepository, IMapper mapper)
        {
            _topicRepository = topicRepository;
            _mapper = mapper;
        }

        public async Task<ICollection<TopicDto>> GetAllTopicsAsync()
        {
            var topics = await _topicRepository.GetAllTopic();
            return _mapper.Map<ICollection<TopicDto>>(topics);   
        }

        public async Task<TopicDto?> GetTopicByIdAsync(int topicId)
        {
            var topic =  await _topicRepository.GetTopicById(topicId);
            return _mapper.Map<TopicDto>(topic);

        }

        public async Task CreateTopicAsync(CreateTopicDto topicDto)
        {
            var topic = _mapper.Map<Topic>(topicDto);
            await _topicRepository.CreateTopic(topic);
        }

        public async Task UpdateTopicAsync(UpdateTopicDto topicDto, int topicId)
        {
            var topic = _mapper.Map<Topic>(topicDto);
            await _topicRepository.UpdateTopic(topic, topicId);
        }

        public async Task DeleteTopicAsync(int topicId)
        {
            await _topicRepository.DeleteTopic(topicId);
        }
    }
}
