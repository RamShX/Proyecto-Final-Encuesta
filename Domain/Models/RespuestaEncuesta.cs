
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    [Table("respuestas_encuestas")]
    public class RespuestaEncuesta
    {
        public int Id { get; set; }
        public int EncuestaId { get; set; }
        public int usuarioId { get; set; }
        [Column("email_responde")]
        public string emailResponde { get; set; }
        [Column("fecha_respuesta")]
        public DateTime FechaRespuesta { get; set; }
        [Column("creado_en")]
        public DateTime creadoEn { get; set; } = DateTime.UtcNow;

        public Encuesta encuesta { get; set; }
        public Usuario usuario { get; set; }
        public ICollection<RespuestaPregunta> RespuestasPreguntas { get; set; } = new List<RespuestaPregunta>();
    }
}
