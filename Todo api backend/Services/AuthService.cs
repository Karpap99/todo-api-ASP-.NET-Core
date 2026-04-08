using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using Todo_api_backend.Config;
using Todo_api_backend.Data.Repositories;
using Todo_api_backend.DTOs.Auth;
using Todo_api_backend.DTOs.User;
using Todo_api_backend.Interfaces.Repositories;
using Todo_api_backend.Interfaces.Services;
using Todo_api_backend.Models;

namespace Todo_api_backend.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _repository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly JwtSettings _jwtConfig;

        public AuthService(IUserRepository repository, IPasswordHasher<User> passwordHasher, IOptions<JwtSettings> jwtConfig)
        {
            _repository = repository;
            _passwordHasher = passwordHasher;
            _jwtConfig = jwtConfig.Value;
        }

        public async Task<BaseResponseDTO?> LoginAsync(AuthLoginDTO user)
        {

            var _user = await _repository.GetByEmailAsync(user.Email);

            if (_user == null)
            {
                throw new InvalidOperationException("User didnt exists");
            }

            var ispasswordValid = _passwordHasher.VerifyHashedPassword(_user, _user.PasswordHash, user.Password);

            if (ispasswordValid == PasswordVerificationResult.Failed)
            {
                throw new InvalidOperationException("Invalid password");
            }

            var token = await GetToken(_user);

            var userResponse = new UserResponseDTO
            {
                Id = _user.Id,
                Email = _user.Email,
            };

            return new BaseResponseDTO
            {
                Token = token,
                User = userResponse
            };
        }

        public async Task<BaseResponseDTO> RegisterAsync(AuthRegisterDTO dto)
        {
            var existing = await _repository.GetByEmailAsync(dto.Email);
            if (existing != null) throw new InvalidOperationException("User already exists");

            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = dto.Email,
            };

            user.PasswordHash = _passwordHasher.HashPassword(user, dto.Password);

            var createdUser = await _repository.Add(user);

            var token = await GetToken(createdUser);

            var userResponse = new UserResponseDTO
            {
                Id = createdUser.Id,
                Email = createdUser.Email,
            };

            return new BaseResponseDTO
            {
                Token = token,
                User = userResponse,
            };
        }

        public async Task<string> GetToken(User user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_jwtConfig.Key)
            );

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtConfig.Issuer,
                audience: _jwtConfig.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
