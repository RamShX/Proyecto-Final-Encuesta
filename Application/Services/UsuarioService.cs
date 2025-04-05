using Domain.Interfeces;
using Domain.Models;
using System.Collections;

namespace Application.Services
{
    public class UsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }
        public async Task<IEnumerable> GetAllUsuarios()
        {
            return await _usuarioRepository.GetAllUsuarios();
        }
        public async Task<Usuario> AddUsuario(Usuario entity)
        {
            return await _usuarioRepository.AddUsuario(entity);
        }
    }
}
