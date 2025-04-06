using Domain.Interfeces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Pensistence.Context;

namespace Pensistence.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly EncuestaContext _context;

        public UsuarioRepository(EncuestaContext context)
        {
            _context = context;
        }

        public async Task<Usuario> AddUsuario(Usuario usuario)
        {
             _context.Usuarios.Add(usuario);
            _context.SaveChanges();
            return await Task.FromResult(usuario);

        }

        public async Task<IEnumerable<Usuario>> GetAllUsuarios()
        {
            var usuarios = await _context.Usuarios.ToListAsync();
            return usuarios;

        }
    }
}
