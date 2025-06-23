using ForumWebsite.Dtos.Topics;
using ForumWebsite.Models;
using ForumWebsite.Services.Topics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ForumWebsite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopicsController : ControllerBase
    {
        private readonly ITopicService _topicService;
        public TopicsController(ITopicService topicService)
        {
            _topicService = topicService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllTopic()
        {
            var result = await _topicService.GetAllTopicsAsync();
            return Ok(result);
        }

        [HttpGet("get-by-id")]
        public async Task<IActionResult> GetTopicById(int topicId)
        {
            var result = await _topicService.GetTopicByIdAsync(topicId);
            return Ok(result);
        }

        [HttpPost("create-topic")]
        public async Task<IActionResult> CreateTopic([FromBody] CreateTopicDto topicDto)
        {
            await _topicService.CreateTopicAsync(topicDto);
            return Ok();
        }

        [HttpDelete("delete-topic")]
        public async Task<IActionResult> DeleteTopic(int topicId)
        {
            await _topicService.DeleteTopicAsync(topicId);
            return Ok();
        }

        [HttpPut("update-topic")]
        public async Task<IActionResult> UpdateTopic(int topicId, [FromBody] UpdateTopicDto topicDto)
        {
            await _topicService.UpdateTopicAsync(topicDto, topicId);
            return Ok();
        }
    }
}
