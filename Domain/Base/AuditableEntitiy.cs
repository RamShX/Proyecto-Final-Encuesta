

using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Base
{
    public abstract class AuditableEntitiy
    {
        [Column("creado_en")]
        public DateTime CreadoEn { get; set; }
        [Column("actualizado_en")]
        public DateTime ActualizadoEn { get; set; }
        
    }
}
