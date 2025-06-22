using AutoMapper;
using ForumWebsite.Dtos.Comments;
using ForumWebsite.Dtos.ForumThreads;
using ForumWebsite.Dtos.Auths;
using ForumWebsite.Models;
using ForumWebsite.Dtos.Users;
using ForumWebsite.Dtos.Votes;

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
            CreateMap<CurrentUserDto, User>();
            CreateMap<User, UpdateCurrentUserDto>();
            CreateMap<UpdateCurrentUserDto, User>();
            CreateMap<PublicUserDto, User>();
            CreateMap<User, PublicUserDto>();
            CreateMap<AdminUserDto, User>();
            CreateMap<User, AdminUserDto>();
            CreateMap<UserBaseDto, User>();
            CreateMap<User, UserBaseDto>();

            CreateMap<ForumThread, ForumThreadDto>();
            CreateMap<CreateForumThreadDto, ForumThread>();


            //CreateMap<ForumThreadVoteDto, ForumThreadVote>();
            //CreateMap<CommentVoteDto, CommentVote>();
        }
    }
}
