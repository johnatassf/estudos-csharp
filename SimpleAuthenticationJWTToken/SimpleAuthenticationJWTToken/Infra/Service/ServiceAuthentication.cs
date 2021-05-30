using EstudoAutenticacao.dbcontext;
using EstudoAutenticacao.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using SimpleAuthenticationJWTToken.Infra.Auxiliary;
using SimpleAuthenticationJWTToken.Infra.Service;
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
        private IServiceManageUser _serviceManageUser;
        private readonly EstudoDbContext _context;
        public ServiceAuthentication(IConfiguration config, IServiceManageUser serviceManageUser)
        {
            _configuration = config;
            _serviceManageUser = serviceManageUser;
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
            var user = await _serviceManageUser.GetUserByEmail(email);
            
            var passwordPassed = new ManagePassword().ValidatePassword(password, user.Hash);

            if (!(user is null) && passwordPassed)
                return user;

            return null;
        }
    }
}
