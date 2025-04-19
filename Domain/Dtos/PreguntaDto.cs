

namespace Domain.Dtos
{
    public class PreguntaDto
    {
        public int Id { get; set; }
        public string Texto { get; set; }
        public TipoP TipoPregunta { get; set; } // 1: Opcion Unica, 2: Opcion Multiple
        public int Orden { get; set; } //Orden de la pregunta en la encuesta
        public bool Obligatorio { get; set; }
        public int EncuestaId { get; set; } //Llave foranea de la tabla encuesta
        public enum TipoP
        {
            opcionMultiple = 1,
            escala = 2
        }
    }
}
