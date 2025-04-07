using Domain.Models;

namespace Domain.Interfeces
{
    public interface IUsuarioRepository

    {
        Task<IEnumerable<Usuario>> GetAllUsuarios();
        Task<Usuario> GetByEmail(string email);
        Task<bool> ExisteEmail(string email);
        Task<Usuario> AddUsuario(Usuario usuario);

    }
}
