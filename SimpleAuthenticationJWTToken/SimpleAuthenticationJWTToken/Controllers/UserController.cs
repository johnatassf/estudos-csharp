using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleAuthenticationJWTToken.Dominio.Dto;
using SimpleAuthenticationJWTToken.Infra.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleAuthenticationJWTToken.Controllers
{
    [Route("user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IServiceManageUser _serviceManageUser;

        public UserController(IServiceManageUser serviceManageUser)
        {
            _serviceManageUser = serviceManageUser;
        }

        [HttpGet()]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(await _serviceManageUser.GetUsers());
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById([FromRoute] long id)
        {
            var user = await _serviceManageUser.GetUserById(id);

            if (user is null)
                return NotFound("Usuário não encontrado");

            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(UserDto userDto)
        {
            try
            {
                var user = await _serviceManageUser.CreateUser(userDto);
                return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
            }
            catch (Exception ex)
            {
                var exception = ex;

                return BadRequest("Erro ao criar o usuário");
            }
          
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(UserDto userDto, [FromRoute] long id)
        {
            if (id != userDto.Id)
                return BadRequest();

            var user = await _serviceManageUser.GetUserById(id);
            
            if (user is null)
                return NotFound("Usuário não encontrado");

            try
            {
               await _serviceManageUser.UpdateUser(userDto, id);
            }catch(Exception ex)
            {
                return BadRequest("Erro ao atualizar o usuário");
            }

            return NoContent();

        }


    }
}
