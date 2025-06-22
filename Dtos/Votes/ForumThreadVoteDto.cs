using ForumWebsite.Dtos.Users;
using ForumWebsite.Models;

namespace ForumWebsite.Dtos.Votes
{
    public class ForumThreadVoteDto
    {
        public int Reputation {  get; set; }
        public int CountUpVote { get; set; }
        public int CountDownVote { get; set; }
        public ICollection<UserBaseDto>? UsersVoteUp { get; set; }
        public ICollection<UserBaseDto>? UsersVoteDown { get; set; }
    }
    
}
