
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Dtos
{
    public class UsuarioLoginDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public int RolId { get; set; }
    }
}
