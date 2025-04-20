using Domain.Enum;

namespace Domain.Dtos
{
    public class PreguntaUpdateDto
    {
        public int Id { get; set; }
        public string Texto { get; set; }
        public TipoPregunta TipoPregunta { get; set; }
        public int Orden { get; set; }
        public bool Obligatoria { get; set; }
        public List<OpcionRespuestaUpdateDto> OpcionesRespuesta { get; set; }
    }
}
