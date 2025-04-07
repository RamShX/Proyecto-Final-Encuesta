
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Dtos
{
    public class RegistrarUsuarioDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmacionPassword { get; set; }
        public int RolId { get; set; }
    }
}
