﻿using Domain.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    [Table("Roles")]
    public class Rol : AuditableEntitiy
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        //Navegacion rol puede tener muchos usuarios e inicializado vacia
        public virtual ICollection<Usuario> Usuarios { get; set; } = new HashSet<Usuario>();
    }
}
