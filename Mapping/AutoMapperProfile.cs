using AutoMapper;
using ForumWebsite.Dtos.Comments;
using ForumWebsite.Dtos.ForumThreads;
using ForumWebsite.Dtos.Auths;
using ForumWebsite.Models;
using ForumWebsite.Dtos.Users;
using ForumWebsite.Dtos.Votes;
using ForumWebsite.Dtos.Topics;
using ForumWebsite.Dtos.Tags;

namespace ForumWebsite.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {

            CreateMap<Comment, CommentDto>();
            CreateMap<CreateCommentDto, Comment>();
            CreateMap<ReplyCommentDto, Comment>();
            CreateMap<Comment, ReplyCommentDto>();


            CreateMap<User, AuthUserDto>();
            CreateMap<AuthUserDto, User>();
            CreateMap<LoginSuccessfullyDto, User>();
            CreateMap<User, LoginSuccessfullyDto>();

            CreateMap<User, CurrentUserDto>();
            CreateMap<UpdateCurrentUserDto, User>();
            CreateMap<User, PublicUserDto>();
            CreateMap<User, AdminUserDto>();
            CreateMap<User, UserBaseDto>();

            CreateMap<ForumThread, ForumThreadDto>();
            CreateMap<ForumThread, ForumThreadDetailDto>();
            CreateMap<CreateForumThreadDto, ForumThread>();


            CreateMap<Topic, TopicDto>();
            CreateMap<CreateTopicDto, Topic>();
            CreateMap<UpdateTopicDto, Topic>();

            CreateMap<Tag, TagDto>();
            CreateMap<CreateTagDto, Tag>();
            CreateMap<UpdateTagDto, Tag>(); 

            //CreateMap<ForumThreadVoteDto, ForumThreadVote>();
            //CreateMap<CommentVoteDto, CommentVote>();
        }
    }
}
