

namespace Domain.Dtos
{
    public class CrearNotificacionDto
    {
        public int EncuestaId { get; set; }
        public int UsuarioId { get; set; }
        public string Tipo { get; set; }
        public string Mensaje { get; set; }
    }
}
