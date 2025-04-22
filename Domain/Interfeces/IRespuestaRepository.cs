
using Domain.Models;

namespace Domain.Interfeces
{
    public interface IRespuestaRepository
    {
        Task<RespuestaEncuesta> GuardarRespuesta(RespuestaEncuesta respuesta);
        Task<IEnumerable<RespuestaEncuesta>> GetRespuestaByEncuestaId(int encuestaId);
        Task<IEnumerable<RespuestaPregunta>> GetRespuestasPreguntaByPreguntaId(int preguntaId);
        Task<bool> ExisteRespuestaUsuario(int encuestaId, int usuarioId);
        Task<RespuestaEncuesta>GetById(int id);


    }
}
