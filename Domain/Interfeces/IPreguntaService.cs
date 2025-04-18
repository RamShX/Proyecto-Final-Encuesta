using Domain.Models;

namespace Domain.Interfeces
{
    public interface IPreguntaService
    {
        Task<bool> CrearPregunta(Pregunta pregunta);
        Task<bool> ActualizarPregunta(Pregunta pregunta);
        Task<bool> EliminarPregunta(int id);
        Task<List<Pregunta>> GetPreguntasByEncuestaId(int encuestaId);
    }
}
