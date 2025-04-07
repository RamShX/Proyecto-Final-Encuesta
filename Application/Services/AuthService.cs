
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

       

        public async Task<UsuarioRespuestaDto> LoginUser(LoginUsuarioDto loginUsuarioDto)
        {
            // Validar si el email existe y validar la contraseña
            var usuario = await _usuarioRepository.GetByEmail(loginUsuarioDto.Email);

            if (usuario == null || !VerifyPasswordHash(loginUsuarioDto.Password, usuario.PasswordHash))
                throw new ArgumentException("Credenciales inválidas");

            // Validar si el usuario está activo
            if (!usuario.Activo)
                throw new ArgumentException("El usuario no está activo");
            

            //Mapear el DTO a la entidad de UsuarioRespuesta
            UsuarioRespuestaDto dto = new UsuarioRespuestaDto
            {
                Email = loginUsuarioDto.Email,
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                RolId = usuario.RolId,
                Id = usuario.Id
            };

            return dto;

        }



        public async Task<UsuarioRespuestaDto> RegisterUser(RegistrarUsuarioDto registrarUsuarioDto)
        {
            // Validar si el email ya existe
            var existeEmail = await _usuarioRepository.ExisteEmail(registrarUsuarioDto.Email);
            if (existeEmail)
                throw new ArgumentException("El email ya está en uso");

            // Validar si las contraseñas coinciden
            if (registrarUsuarioDto.Password != registrarUsuarioDto.ConfirmacionPassword)
                throw new ArgumentException("Las contraseñas no coinciden");

            //Mapar el DTO a la entidad usuario
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
            return BCrypt.Net.BCrypt.Verify(password, storedHash);
        }
    }
}
