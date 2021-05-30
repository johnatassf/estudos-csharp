using EstudoAutenticacao.dbcontext;
using EstudoAutenticacao.Model;
using Microsoft.EntityFrameworkCore;
using SimpleAuthenticationJWTToken.Dominio.Dto;
using SimpleAuthenticationJWTToken.Infra.Auxiliary;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleAuthenticationJWTToken.Infra.Service
{
    public class ServiceManageUser : IServiceManageUser
    {
        private readonly EstudoDbContext _context;

        public ServiceManageUser(EstudoDbContext context)
        {
            _context = context;
        }

        public async Task<User> CreateUser(UserDto userDto)
        {

            var user = new User()
            {
                Email = userDto.Email,
                Login = userDto.Login,
                Nome = userDto.Nome,
                Hash = new ManagePassword().GetPasswordHash(userDto.Senha)
            };


            _context.Add(user);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                //Add in LOG              
                throw;
            }

            return user;

        }
        public async Task<User> UpdateUser(UserDto userDto, long id)
        {
            var user = await GetUserById(id);

            user.Login = userDto.Login;
            user.Email = userDto.Email;
            user.Nome = userDto.Nome;

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex) when (!UserExists(id))
            {
                // Add in log
                throw;
            }

            return user;
        }
        public async Task<User> GetUserById(long id)
        {
            return await _context.Users.FindAsync(id); ;
        }
        public async Task<IEnumerable<User>> GetUsers()
        {
                   return await _context.Users.ToListAsync();
        }

        private bool UserExists(long id) =>
            _context.Users.Any(e => e.Id == id);

        public async Task<User> GetUserByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
