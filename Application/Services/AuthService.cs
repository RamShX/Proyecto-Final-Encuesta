
using Domain.Dtos;
using Domain.Interfeces;
using Domain.Models;

namespace Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public AuthService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<string> LoginUser(LoginUsuarioDto loginUsuarioDto)
        {
            throw new NotImplementedException();
        }

        public async Task<UsuarioRespuestaDto> RegisterUser(RegistrarUsuarioDto registrarUsuarioDto)
        {
            if (registrarUsuarioDto.Password != registrarUsuarioDto.ConfirmacionPassword)
                throw new ArgumentException("Las contraseñas no coinciden");

            var usuario = new Usuario
            {
                Nombre = registrarUsuarioDto.Nombre,
                Apellido = registrarUsuarioDto.Apellido,
                Email = registrarUsuarioDto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(registrarUsuarioDto.Password),
                RolId = registrarUsuarioDto.RolId,
                Activo = true,
                CreadoEn = DateTime.UtcNow,
                ActualizadoEn = DateTime.UtcNow
            };

            // darle respuesta al Frontend
            var UsuarioRespuestaDto = new UsuarioRespuestaDto
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                Email = usuario.Email,
                RolId = usuario.RolId,
            };
            await _usuarioRepository.AddUsuario(usuario);
            return UsuarioRespuestaDto;
        }

        public bool VerifyPasswordHash(string password, string storedHash)
        {
            throw new NotImplementedException();
        }
    }
}
