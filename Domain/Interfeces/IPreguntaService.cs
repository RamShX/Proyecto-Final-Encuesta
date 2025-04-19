using Domain.Dtos;

namespace Domain.Interfeces
{
    public interface IPreguntaService
    {
        Task<int> CrearPregunta(PreguntaDto pregunta);
        Task<bool> ActualizarPregunta(int id, PreguntaDto pregunta);
        Task<bool> EliminarPregunta(int id);
        Task<List<PreguntaDto>> GetPreguntasByEncuestaId(int encuestaId);
    }
}
