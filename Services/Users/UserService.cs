using AutoMapper;
using ForumWebsite.Dtos.Users;
using ForumWebsite.Repositories.Users;
using ForumWebsite.Repositories.Votes;

namespace ForumWebsite.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<CurrentUserDto> GetCurrentUser(int userId)
        {
            var currentUser = await _userRepository.GetUserById(userId);
            return _mapper.Map<CurrentUserDto>(currentUser);
        }
        public async Task UpdateCurrentUser(UpdateCurrentUserDto updateCurrentUserDto, int userId)
        {
            var currentUser = await _userRepository.GetUserById(userId);
            _mapper.Map(updateCurrentUserDto, currentUser);
            await _userRepository.UpdateUser(currentUser);
        }


        public async Task<PublicUserDto> GetUserById(int userId)
        {
            var publicUser = await _userRepository.GetUserById(userId);
            return _mapper.Map<PublicUserDto>(publicUser);
        }

        public async Task<ICollection<AdminUserDto>> GetAllUser()
        {
            var userList = await _userRepository.GetAllUsers();
            return _mapper.Map<ICollection<AdminUserDto>>(userList);
        }
        public async Task DeleteUser(int userId)
        {
            await _userRepository.DeleteUser(userId);
        }
        public async Task BanUser(int userId)
        {
            var publicUser = await _userRepository.GetUserById(userId);
            publicUser.IsBanned = true;
            await _userRepository.UpdateUser(publicUser);
        }
    }
}
