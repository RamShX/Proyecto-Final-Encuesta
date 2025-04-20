

using Domain.Enum;

namespace Domain.Dtos
{
    public class PreguntaDto
    {
        public int Id { get; set; }
        public string Texto { get; set; }
        public TipoPregunta TipoPregunta { get; set; } // 1: Opcion Multiple, 2: escala.
        public int Orden { get; set; } //Orden de la pregunta en la encuesta
        public bool Obligatorio { get; set; }
        public int EncuestaId { get; set; } //Llave foranea de la tabla encuesta

        public List<OpcionRespuestaDto> OpcionesRespuesta { get; set; }

    }
}
