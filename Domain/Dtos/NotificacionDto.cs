

namespace Domain.Dtos
{
    public class NotificacionDto
    {
        public int Id { get; set; }
        public int EncuestaId { get; set; }
        public int UsuarioId { get; set; }
        public string Tipo { get; set; }
        public string Mensaje { get; set; }
        public DateTime CreadoEn { get; set; }

    }
}
