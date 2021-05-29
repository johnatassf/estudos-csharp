using EstudoAutenticacao.dbcontext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EstudoAutenticacao.Infra.Service
{
    public class ServiceAuthentication
    {
        public IConfiguration _configuration;
        private readonly EstudoDbContext _context;
        public ServiceAuthentication(IConfiguration config, EstudoDbContext context)
        {
            _configuration = config;
            _context = context;
        }
        public string AuthorizationTokenUser(User user)
        {
            if (user is null)
                return null;

            var claims = GetClaims(user);

            return GetJWTToken(claims);
        }

        private IEnumerable<Claim> GetClaims(User user)
        {
            var claims = new[] {
                    new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim("Id", user.Id.ToString()),
                    new Claim("Nome", user.Nome),
                    new Claim("Login", user.Login ?? string.Empty),
                    new Claim("Email", user.Email)
                   };

            return claims;
        }
        public string GetJWTToken(IEnumerable<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                         _configuration["Jwt:Audience"], 
                         claims,
                         expires: DateTime.UtcNow.AddDays(1),
                         signingCredentials: signIn);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public async Task<User> AuthenticationUser(string email, string password)
        {
            if (email == "email@gmail.com" && password == "123456")
                return new User() { Id = 1, Email = "email@gmail.com", Nome = "Usuario" };

            return null;
        }
    }
}
