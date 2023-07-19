using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Dominio.Entities;
using Dominio.Dto;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace api_base_autenticacao.Controllers
{
    [Controller]
    [Route("authentication")]
    public class UserManagerController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManage;

        public UserManagerController(SignInManager<User> signInManage,
           UserManager<User> userManager
           )
        {
            _userManager = userManager;
            _signInManage = signInManage;
        }


        [HttpPost("user")]
        public async Task<IActionResult> CreateUser(UserManagerDto userManagerDto)
        {
            var user = await _userManager.FindByEmailAsync(userManagerDto.UserName);

            if (user == null)
            {
                user = new User
                {
                    FirstName = userManagerDto.FirstName,
                    LastName = userManagerDto.LastName,
                    Email = userManagerDto.Email,
                    UserName = userManagerDto.UserName,
                };

                var result = await _userManager.CreateAsync(user, "P@ssW0rd!");

                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create a user");
                }
                else
                {
                    return Ok("User create with success");
                }
            }

            return BadRequest("Create failed");


        }

        [HttpPost("token")]
        public async Task<IActionResult> CreateToken(LoginUserDto loginUserDto)
        {
         if(!ModelState.IsValid)
            return BadRequest("Verifique os dados informados");


            var user = await _userManager.FindByEmailAsync(loginUserDto.Username);

            if(user is not null)
            {
                var result = await _signInManage.CheckPasswordSignInAsync(user, loginUserDto.Password, false);

                if (result.Succeeded)
                {
                    var claims = new Claim[]
                       {
                            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                            new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
                       };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("1234567810soidfjiosdjfoisadf"));
                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(
                        "http://localhost:8888",
                        "http://localhost:8888",
                        claims,
                        signingCredentials: creds,
                        expires: DateTime.UtcNow.AddMinutes(20));


                    return Created("", new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        expiration = token.ValidTo
                    });
                }
            }

            return BadRequest("Erro ao efetuar login");


        }
    }
}
