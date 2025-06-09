using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RestaurantAPI.Application.Interfaces;
using RestaurantAPI.Configuration;
using RestaurantAPI.Domain.Interfaces;
using RestaurantAPI.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAPI.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _hasher;
        private readonly JwtSettings _jwtSettings;

        public AuthService(IUserRepository userRepository, IOptions<JwtSettings> jwtOptions, IPasswordHasher<User> hasher)
        {
            _userRepository = userRepository;
            _hasher = hasher;
            _jwtSettings = jwtOptions.Value;
        }

        public async Task<string> LoginAsync(string username, string password)
        {
            var users = await _userRepository.GetAllAsync();
            var user = users.FirstOrDefault(u => u.Username == username);

            if (user == null || !VerifyPassword(user, password, user.PasswordHash))
                throw new UnauthorizedAccessException("Invalid username or password.");

            return GenerateJwtToken(user);
        }

        private bool VerifyPassword(User user,string password, string hashed)
        {
            return _hasher.VerifyHashedPassword(user,hashed, password) == PasswordVerificationResult.Success;
        }

        private string GenerateJwtToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim("role_id", user.RoleId.ToString())
        };

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiresInMinutes),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
