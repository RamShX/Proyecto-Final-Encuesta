using System.ComponentModel.DataAnnotations.Schema;


namespace Domain.Models
{
    public class Rol
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        [Column("creado_en")]
        public DateTime CreadoEn { get; set; }
        [Column("actualizado_en")]
        public DateTime ActualizadoEn { get; set; }

        //Navegacion rol puede tener muchos usuarios e inicializado vacia
        public virtual ICollection<Usuario> Usuarios { get; set; } = new HashSet<Usuario>();
    }
}
