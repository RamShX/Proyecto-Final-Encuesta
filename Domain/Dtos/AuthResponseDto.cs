
namespace Domain.Dtos
{
    public class AuthResponseDto
    {
        public string Token { get; set; }
        public DateTime Expiracion { get; set; }
        public UsuarioRespuestaDto Usuario { get; set; }
    }
}
