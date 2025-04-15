using Domain.Base;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Models
{
    public class Usuario : AuditableEntitiy
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        [Column("password_hash")]
        public string PasswordHash { get; set; }
        [Column("rol_id")]
        public int RolId { get; set; } //Llave foranea de la tabla rol
        public bool Activo { get; set; } = true;


        //Navegacion usuario puede tener un rol
        [JsonIgnore]
        public virtual Rol Rol { get; set; }
        //Navegacion usuario puede tener muchas encuestas e inicializarla vacía
        public virtual ICollection<Encuesta> Encuestas { get; set; } = new HashSet<Encuesta>();

    }
}
