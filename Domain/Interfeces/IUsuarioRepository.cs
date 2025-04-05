using Domain.Models;

namespace Domain.Interfeces
{
    public interface IUsuarioRepository

    {
        Task<IEnumerable<Usuario>> GetAllUsuarios();
        Task<Usuario> AddUsuario(Usuario usuario);

    }
}
