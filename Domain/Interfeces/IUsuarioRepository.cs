
using Domain.Models;
using Domain.Dtos;

namespace Domain.Interfeces
{
    public interface IUsuarioRepository

    {
        Task<IEnumerable<UsuarioResponseDto>> GetAllUsuarios();
        Task<Usuario> GetByEmail(string email);
        Task<bool> ExisteEmail(string email);
        Task<Usuario> AddUsuario(Usuario usuario);

    }
}
