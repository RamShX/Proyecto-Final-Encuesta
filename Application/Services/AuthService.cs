
using AutoMapper;
using Domain.Dtos;
using Domain.Interfeces;
using Domain.Models;

namespace Application.Services
{

    public class AuthService : IAuthService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IJwtService _jwtService;
        private readonly IMapper _mapper;

        public AuthService(IUsuarioRepository usuarioRepository,IJwtService service, IMapper mapper)
        {
            _usuarioRepository = usuarioRepository;
            _jwtService = service;
            _mapper = mapper;
        }

       

        public async Task<AuthResponseDto> LoginUser(LoginUsuarioDto loginUsuarioDto)
        {
            // Validar la contraseña y el email
            var usuario = await _usuarioRepository.GetByEmail(loginUsuarioDto.Email);

            if (usuario == null || !VerifyPasswordHash(loginUsuarioDto.Password, usuario.PasswordHash))
                throw new ArgumentException("Credenciales inválidas");

            // Validar si el usuario está activo
            if (!usuario.Activo)
                throw new ArgumentException("El usuario no está activo");
            
            var authResult = await _jwtService.GenerarToken(usuario);

            //Mapear el DTO a la entidad de UsuarioRespuesta
            AuthResponseDto authResponseDto = new AuthResponseDto
            {
                Token = authResult,
                Expiracion = DateTime.Now.AddMinutes(3),
                Usuario = _mapper.Map<UsuarioResponseDto>(usuario)


            };

            return authResponseDto;

        }



        public async Task<UsuarioResponseDto> RegisterUser(RegistrarUsuarioDto registrarUsuarioDto)
        {
            // Validar si el email ya existe
            var existeEmail = await _usuarioRepository.ExisteEmail(registrarUsuarioDto.Email);
      
            if (existeEmail)
                throw new ArgumentException("El email ya está en uso");

            // Validar si las contraseñas coinciden
            if (registrarUsuarioDto.Password != registrarUsuarioDto.ConfirmacionPassword)
                throw new ArgumentException("Las contraseñas no coinciden");

            // Validar que la contraseña tenga al menos 6 caracteres
            if (registrarUsuarioDto.Password.Length < 6)
                throw new ArgumentException("La contraseña debe tener al menos 6 caracteres");

            //Mapar el DTO a la entidad usuario
            var usuario = new Usuario
            {
                Nombre = registrarUsuarioDto.Nombre,
                Apellido = registrarUsuarioDto.Apellido,
                Email = registrarUsuarioDto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(registrarUsuarioDto.Password),
                RolId = 2,// siempre se registrará como usuario estandar
                Activo = true,
                CreadoEn = DateTime.Now,
                ActualizadoEn = DateTime.Now
            };

            // darle respuesta al Frontend
            var UsuarioRespuestaDto = new UsuarioResponseDto
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                Email = usuario.Email
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
