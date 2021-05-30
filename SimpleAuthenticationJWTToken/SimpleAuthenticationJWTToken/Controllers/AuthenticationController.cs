using EstudoAutenticacao.dbcontext;
using EstudoAutenticacao.Infra.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SimpleAuthenticationJWTToken.Infra.Service;
using System.Threading.Tasks;

namespace EstudoAutenticacao.Controllers
{
    [Route("authentication/token")]
    [ApiController]
    public class AuthenticationController : Controller
    {
        public IConfiguration _configuration;
        private readonly ServiceAuthentication _serviceAuthentication;
        private readonly IServiceManageUser _serviceManageUser;

        public AuthenticationController(IConfiguration config, IServiceManageUser serviceManageUser)
        {
            _configuration = config;
            _serviceAuthentication = new ServiceAuthentication(config, serviceManageUser);
            _serviceManageUser = serviceManageUser;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> AuthenticateUser(string email, string senha)
        {
            var user = await _serviceManageUser.GetUserByEmail(email);

            if(user is null)
                return BadRequest("Usuário ou senha inválidos");

             user = await _serviceAuthentication.AuthenticationUser(email, senha);

            if (user is null)
                return BadRequest("Usuário ou senha inválidos");

            return Ok(_serviceAuthentication.AuthorizationTokenUser(user));

        }


    }
}
