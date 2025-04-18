using Domain.Dtos;
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

        public async Task<bool> ExisteEmail(string email)
        {
            return await _context.Usuarios.AnyAsync(u => u.Email == email);
        }

        public async Task<IEnumerable<UsuarioResponseDto>> GetAllUsuarios()
        {
            var usuarios = await _context.Usuarios
                .Include(u => u.Rol)
                .Select(u => new UsuarioResponseDto
                {
                    Id = u.Id,
                    Nombre = u.Nombre,
                    Apellido = u.Apellido,
                    Email = u.Email,
                    RolNombre = u.Rol.Nombre,
                })
                .ToListAsync();
            return usuarios;

        }

        public async Task<Usuario> GetByEmail(string email)
        {
            var usuario = await _context.Usuarios
                                                .Include(u => u.Rol)
                                                .FirstOrDefaultAsync(u => u.Email == email);
            if (usuario == null)
                throw new KeyNotFoundException("El usuario no existe");
            return usuario;
        }
    }
}
