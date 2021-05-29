using EstudoAutenticacao.dbcontext;
using EstudoAutenticacao.Infra.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

using System.Threading.Tasks;

namespace EstudoAutenticacao.Controllers
{
    [Route("authentication/token")]
    [ApiController]
    public class AuthenticationController : Controller
    {
        public IConfiguration _configuration;
        private readonly ServiceAuthentication _serviceAuthentication;

        public AuthenticationController(IConfiguration config)
        {
            _configuration = config;
            _serviceAuthentication = new ServiceAuthentication(config, new EstudoDbContext());
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> AuthenticateUser(string email, string senha)
        {
            var user = await _serviceAuthentication.AuthenticationUser(email, senha);

            if (user is null)
                return BadRequest("Usuário ou senha inválidos");

            return Ok(_serviceAuthentication.AuthorizationTokenUser(user));

        }

        [HttpGet("get")]
        public async Task<IActionResult> AuthenticateUser()
        {
           
            return Ok("Usuário ou senha inválidos");

        }

    }
}
