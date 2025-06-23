using ForumWebsite.Dtos.ForumThreads;

namespace ForumWebsite.Services.ForumThreads
{
    public interface IForumThreadService
    {
        Task<ICollection<ForumThreadDto>> GetThreads();
        Task<ForumThreadDetailDto> GetThreadById(int threadId);
        Task<ICollection<ForumThreadDto>> SearchThreadByName(string name);
        Task<ICollection<ForumThreadDto>> FillterThreadByTopicAndTag(int? topicId, List<int> tagIds);
        Task<ForumThreadDto> CreateThread(CreateForumThreadDto dto, int userId);
        Task UpdateThread(int threadId, UpdateForumThreadDto updateForumThreadDto, int userId);
        Task DeleteThread(int threadId, int userId);

    }
}
