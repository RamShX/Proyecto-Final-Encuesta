

using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    [Table("respuestas_pregunta")]
    public class RespuestaPregunta
    {
        public int Id { get; set; }
        public int RespuestaEncuestaId { get; set; }
        public int PreguntaId { get; set; }
        public int? OpcionId { get; set; }
        [Column("valor_escala")]
        public int? ValorEscala { get; set; }
        [Column("creado_en")]
        public DateTime CreadoEn { get; set; } = DateTime.UtcNow;

        public virtual RespuestaEncuesta RespuestaEncuesta { get; set; }
        public virtual Pregunta Pregunta { get; set; }
        public virtual OpcionRespuesta Opcion { get; set; }
    }
}
