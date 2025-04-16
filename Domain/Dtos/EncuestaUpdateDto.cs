

namespace Domain.Dtos
{
    public class EncuestaUpdateDto
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public bool EsPublica { get; set; }
        public string EnlacePublico { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaExpiracion { get; set; }
    }
}
