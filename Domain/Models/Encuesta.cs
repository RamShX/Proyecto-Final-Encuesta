using Domain.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class Encuesta : AuditableEntitiy
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        [Column("es_publica")]
        public bool EsPublica { get; set; }
        [Column("enlace_publico")]
        public string EnlacePublico { get; set; }
        public bool Activo { get; set; }
        [Column("creador_id")]
        public int UsuarioId { get; set; } //Llave foranea de la tabla usuario


        //Navegacion
        public virtual Usuario Creador { get; set; }

        //public virtual ICollection<Pregunta> Preguntas { get; set; } = new List<Pregunta>();
    }
}
