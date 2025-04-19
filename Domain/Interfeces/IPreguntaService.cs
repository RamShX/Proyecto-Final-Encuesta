using Domain.Dtos;

namespace Domain.Interfeces
{
    public interface IPreguntaService
    {
        Task<bool> CrearPregunta(PreguntaDto pregunta);
        Task<bool> ActualizarPregunta(PreguntaDto pregunta);
        Task<bool> EliminarPregunta(int id);
        Task<List<PreguntaDto>> GetPreguntasByEncuestaId(int encuestaId);
    }
}
