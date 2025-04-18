
using Domain.Dtos;
using Domain.Models;

namespace Domain.Interfeces
{
    public interface IAuthService
    {
        Task<UsuarioResponseDto> RegisterUser(RegistrarUsuarioDto registrarUsuarioDto);
        Task<AuthResponseDto> LoginUser(LoginUsuarioDto loginUsuarioDto);
        bool VerifyPasswordHash(string password, string storedHash);
    }
}
