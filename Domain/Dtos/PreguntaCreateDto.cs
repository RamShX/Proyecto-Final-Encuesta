using Domain.Enum;

namespace Domain.Dtos
{
    public class PreguntaCreateDto
    {
        public int EncuestaId { get; set; }
        public string Texto { get; set; }
        public TipoPregunta TipoPregunta { get; set; }
        public int Orden { get; set; }
        public bool Obligatorio { get; set; }
        public List<OpcionRespuestaCreateDto> OpcionesRespuesta { get; set; }
    }
}
