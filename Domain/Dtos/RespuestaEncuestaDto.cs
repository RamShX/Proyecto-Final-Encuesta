

namespace Domain.Dtos
{
    public class RespuestaEncuestaDto
    {
        public int Id { get; set; }
        public int EncuestaId { get; set; }
        public int? UsuariodId { get; set; }
        public string EmailRespondente { get; set; }
        public DateTime FechaRespuesta { get; set; }
        public List<RespuestaPreguntaDto> RespuestasPreguntas { get; set; }
    }
}
