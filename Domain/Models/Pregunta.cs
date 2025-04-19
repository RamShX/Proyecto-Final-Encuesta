
using Domain.Base;
using Domain.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    [Table("Preguntas")]
    public class Pregunta : AuditableEntitiy
    {
        public int Id { get; set; }
        public string Texto { get; set; }
        public TipoPregunta TipoPregunta { get; set; } // 1: Opcion Multiple, 2: escala.
        public int Orden { get; set; } //Orden de la pregunta en la encuesta
        public bool Obligatorio { get; set; }
        public int EncuestaId { get; set; } //Llave foranea de la tabla encuesta

        //Navegacion y relaciones
        public virtual Encuesta Encuesta { get; set; }
        public virtual ICollection<OpcionRespuesta> Opciones { get; set; } = new List<OpcionRespuesta>();

    }


}
