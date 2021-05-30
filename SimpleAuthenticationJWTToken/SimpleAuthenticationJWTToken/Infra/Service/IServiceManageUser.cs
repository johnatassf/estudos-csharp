using EstudoAutenticacao.Model;
using SimpleAuthenticationJWTToken.Dominio.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleAuthenticationJWTToken.Infra.Service
{
    public interface IServiceManageUser
    {
        Task<User> CreateUser(UserDto userDto);
        Task<User> GetUserById(long id);
        Task<User> GetUserByEmail(string email);
        Task<IEnumerable<User>> GetUsers();
        Task<User> UpdateUser(UserDto userDto, long id);

    }
}