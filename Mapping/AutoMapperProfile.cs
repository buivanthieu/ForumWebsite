using AutoMapper;
using ForumWebsite.Dtos.Comments;
using ForumWebsite.Dtos.Users;
using ForumWebsite.Models;

namespace ForumWebsite.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Comment, CommentDto>();
            CreateMap<CreateCommentDto, Comment>();

            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
        }
    }
}
