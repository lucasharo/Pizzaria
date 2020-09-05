using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using System.Linq;
using Domain.Settings;
using Domain.Entities;
using System.Text;
using Domain.Exceptions;

namespace Services.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly SigningCredentials _signingCredentials;

        public TokenService(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;

            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration.GetSection("Security:Key").Value));

            _signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256Signature);
        }

        public string GenerateToken(Usuario user)
        {
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("Id", user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddHours(240),
                SigningCredentials = _signingCredentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public string GenerateTokenChangePassword(long id)
        {
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("Id", id.ToString()),
                    new Claim(ClaimTypes.Role, Role.AlterarSenha)
                }),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = _signingCredentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public string GetToken()
        {
            return _httpContextAccessor.HttpContext.Request.Headers[HeaderNames.Authorization].FirstOrDefault().Replace("Bearer", "").Trim();
        }

        public int GetIdByToken()
        {
            var id = _httpContextAccessor.HttpContext.User?.Claims.FirstOrDefault(c => c.Type == "Id");

            if (id != null)
            {
                return Convert.ToInt32(id.Value);
            }
            else
            {
                throw new AppException("Usuário não identificado no token");
            }
        }
    }
}