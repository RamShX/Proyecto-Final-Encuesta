namespace Domain.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public int RolId { get; set; } //Llave foranea de la tabla rol
        public bool Activo { get; set; }
        public DateTime CreadoEn { get; set; }
        public DateTime ActualizadoEn { get; set; }
        
        //Navegacion usuario puede tener un rol
        public virtual Rol Rol { get; set; }

    }
}
