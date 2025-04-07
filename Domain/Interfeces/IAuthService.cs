
using Domain.Dtos;
using Domain.Models;

namespace Domain.Interfeces
{
    public interface IAuthService
    {
        Task<UsuarioRespuestaDto> RegisterUser(RegistrarUsuarioDto registrarUsuarioDto);
        Task<UsuarioRespuestaDto> LoginUser(LoginUsuarioDto loginUsuarioDto);
        bool VerifyPasswordHash(string password, string storedHash);
    }
}
