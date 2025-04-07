using Domain.Dtos;
using Domain.Interfeces;
using Microsoft.AspNetCore.Mvc;
using WebApi.Response;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUsuarioDto loginUsuarioDto)
        {
            var token = await _authService.LoginUser(loginUsuarioDto);
            return Ok(new ApiResponse<string> (token, "Inició sesión correctamente!"));
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrarUsuarioDto registrarUsuarioDto)
        {
            var usuario = await _authService.RegisterUser(registrarUsuarioDto);
            return CreatedAtAction(nameof(Register), new ApiResponse<UsuarioRespuestaDto> (usuario, "Usuario registrado con éxito"));
        }
    }
        
    
}
