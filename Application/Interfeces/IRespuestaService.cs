

using Domain.Dtos;

namespace Application.Interfeces
{
    public interface IRespuestaService
    {
        Task<RespuestaEncuestaDto> GuardarRespuesta(CrearRespuestaEncuestaDto respuesta);
        Task<IEnumerable<RespuestaEncuestaDto>> GetRespuestaByEncuestaId(int encuestaId);
        Task<RespuestaEncuestaDto> GetById(int id);
        Task<EstadisticasDto> GetEstadisticasByEncuestaId(int encuestaId);
    }
}
