using Domain.Models;

namespace Domain.Interfeces
{
    public interface IUsuarioRepository<T> where T : class

    {
        Task<IEnumerable<T>> GetAllUsuarios();
        Task<Usuario> AddUsuario(T entity);

    }
}
