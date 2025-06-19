using AutoMapper;
using ForumWebsite.Dtos.Auths;
using ForumWebsite.Models;
using ForumWebsite.Repositories.Users;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ForumWebsite.Services.Auths
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public AuthService(IUserRepository userRepository, IMapper mapper, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<AuthUserDto> Register(RegisterDto dto)
        {
            var existingUser = await _userRepository.GetByEmailUser(dto.Email);
            if (existingUser != null)
                throw new ArgumentException("Username already taken.");


            var passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            var user = new User
            {
                Username = dto.Username,
                Email = dto.Email,
                PasswordHash = passwordHash,
                Role = "User",
                DisplayName = dto.Username,
                AvatarUrl = "",
                Reputation = 0,
                TotalComments = 0,
                TotalThreads = 0,
                JoinedAt = DateTime.UtcNow,
                IsBanned = false
            };

            await _userRepository.AddUser(user);

            return _mapper.Map<AuthUserDto>(user);
        }

        public async Task<LoginSuccessfullyDto> Login(LoginDto dto)
        {
            var user = await _userRepository.GetByEmailUser(dto.Email)
                ?? throw new KeyNotFoundException("Email not found");

            if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                throw new UnauthorizedAccessException("Invalid credentials");

            var token = GenerateJwtToken(user);
            var loginSuccessfullyDto = _mapper.Map<LoginSuccessfullyDto>(user);
            loginSuccessfullyDto.Token = token;
            return loginSuccessfullyDto;
        }

        public async Task<AuthUserDto> GetProfile(int userId)
        {
            var user = await _userRepository.GetUserById(userId);
            return _mapper.Map<AuthUserDto>(user);
        }
        private string GenerateJwtToken(User user)
        {
            var jwtSettings = _configuration.GetSection("Jwt");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString(CultureInfo.InvariantCulture)),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Role),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };


            var token = new JwtSecurityToken
                (
                    issuer: jwtSettings["Issuer"],
                    audience: jwtSettings["Audience"],
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(double.Parse(jwtSettings["ExpiresInMinutes"]!, CultureInfo.InvariantCulture)),
                    signingCredentials: creds
                );
            Console.WriteLine("🔐 Secret key khi generate token: " + jwtSettings["Key"]);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

}
