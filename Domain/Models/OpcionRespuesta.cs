using Domain.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    [Table("Opciones_Respuesta")]
    public class OpcionRespuesta : AuditableEntitiy
    {
        public int Id { get; set; }
        public int PreguntaId { get; set; }
        public string Texto { get; set; }
        public int Valor { get; set; }
        public int Orden { get; set; }

        public Pregunta Pregunta { get; set; }
        


    }
}
