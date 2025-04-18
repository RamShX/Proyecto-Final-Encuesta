using Domain.Dtos;
using Domain.Interfeces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Response;

namespace WebApi.Controllers
{
    [AllowAnonymous]
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
        public async Task<ActionResult<AuthResponseDto>> Login([FromBody] LoginUsuarioDto loginUsuarioDto)
        {
            try
            {
                var usuario = await _authService.LoginUser(loginUsuarioDto);
                return Ok(new ApiResponse<AuthResponseDto>(usuario, "Inició sesión correctamente!"));

            }
            catch (ApplicationException e)
            {

                return BadRequest(new ApiResponse<string>{Message = e.Message});
            }
            
        }

        
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrarUsuarioDto registrarUsuarioDto)
        {
            var usuario = await _authService.RegisterUser(registrarUsuarioDto);
            return CreatedAtAction(nameof(Register), new ApiResponse<UsuarioResponseDto> (usuario, "Usuario registrado con éxito"));
        }
    }
        
    
}
