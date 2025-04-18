

using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class Notificacion
    {
        public int Id { get; set; }
        public int EncuestaId { get; set; }
        public int usuarioId { get; set; }
        public string tipo { get; set; }
        public string Mensaje { get; set; }
        [Column("creado_en")]
        public DateTime creadoEn { get; set; } = DateTime.UtcNow;

        public Encuesta Encuesta { get; set; }
        public Usuario Usuario { get; set; }
    }
}
