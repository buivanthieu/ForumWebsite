using ForumWebsite.Dtos.Tags;
using ForumWebsite.Models;
using ForumWebsite.Services.Tags;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ForumWebsite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly ITagService _tagService;
        public TagsController(ITagService tagService)
        {
            _tagService = tagService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllTag()
        {
            var result = await _tagService.GetAllTagsAsync();
            return Ok(result);
        }

        [HttpGet("get-by-id")]
        public async Task<IActionResult> GetTagById(int tagId)
        {
            var result = await _tagService.GetTagByIdAsync(tagId);
            return Ok(result);
        }

        [HttpPost("create-tag")]
        public async Task<IActionResult> CreateTag([FromBody] CreateTagDto tagDto)
        {
            await _tagService.CreateTagAsync(tagDto);
            return Ok();
        }

        [HttpDelete("delete-tag")]
        public async Task<IActionResult> DeleteTag(int tagId)
        {
            await _tagService.DeleteTagAsync(tagId);
            return Ok();
        }

        [HttpPut("update-tag")]
        public async Task<IActionResult> UpdateTag(int tagId, [FromBody] UpdateTagDto tagDto)
        {
            await _tagService.UpdateTagAsync(tagDto, tagId);
            return Ok();
        }
    }
}
