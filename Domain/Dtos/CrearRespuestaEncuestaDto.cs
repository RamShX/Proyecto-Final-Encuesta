

namespace Domain.Dtos
{
    public class CrearRespuestaEncuestaDto
    {
        public int EncuestaId { get; set; }
        public int? UsuariodId { get; set; }
        public string EmailRespondente { get; set; }
        public List<CrearRespuestaPreguntaDto> RespuestasPreguntas { get; set; }
    }
}
