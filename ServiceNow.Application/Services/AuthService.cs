using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ServiceNow.ServiceNow.Application.DTOs;
using ServiceNow.ServiceNow.Application.Interfaces;
using ServiceNow.ServiceNow.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ServiceNow.ServiceNow.Application.Services
{
    public class AuthService : IAuthService
    {
        public readonly IUserRepository userRepository;
        public readonly IConfiguration _configuration;

        public AuthService(IUserRepository userRepository, IConfiguration configuration)
        {
            this.userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<LoginResponse> LoginAsync(string email, string password)
        {
            if (string.IsNullOrEmpty(email))
            {
                return new LoginResponse { Success = false, Message = "Empty UserName or password" };
            }
            var user = await userRepository.getEmailbyAsync(email);
            if (user != null)
            {
                if (String.Equals(user.Password, password))
                {
                    var token = GenerateToken(user);


                    return new LoginResponse { Success = true, Token=token };
                }

            }


            return new LoginResponse { Success = false, Message = "Invalid UserName or password" };

        }
        public string GenerateToken(User user)
        {
            var issuer = _configuration["JwtConfig:Issuer"];
            var audience = _configuration["JwtConfig:Audience"];
            var key = _configuration["JwtConfig:Key"];
            var tokenValidityMins = _configuration.GetValue<int>("JwtConfig:TokenValidityMins");

            var tokenExpiryTimeStamp = DateTime.UtcNow.AddMinutes(tokenValidityMins);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role)
            // Add when roles are implemented
            // new Claim(ClaimTypes.Role, user.Role)
        }),

                Expires = tokenExpiryTimeStamp,
                Issuer = issuer,
                Audience = audience,

                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(key!)
                    ),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var securityToken = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(securityToken);
        }

        public async Task<CreateUserResponse> CreateUserAsync(string username, string password, string email)
        {
            if (string.IsNullOrEmpty(username))
            {

                return new CreateUserResponse { Success = false, Message = "Invalid username" };

            }
            if (string.IsNullOrEmpty(email))
            {
                return new CreateUserResponse { Success = false, Message = "Invalid email" };
            }
            var user = await userRepository.getEmailbyAsync(email);
            if (user != null)
            {
                return new CreateUserResponse { Success = false, Message = "User already exists for this email id" };
            }

            bool result = await userRepository.createUserAsyncDb(username, password, email);
            if (result == false)
            {
                return new CreateUserResponse { Success = true, Message = "User created successfully" };
            }

            return new CreateUserResponse { Success = true, Message = "User created successfully" };

        }
    }
}