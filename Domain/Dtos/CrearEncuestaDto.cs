
namespace Domain.Dtos
{
    public class CrearEncuestaDto
    {
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public bool EsPublica { get; set; }
        public string EnlacePublico { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaExpiracion { get; set; }
        public int UsuarioId { get; set; } //Llave foranea de la tabla usuario
    }
}
