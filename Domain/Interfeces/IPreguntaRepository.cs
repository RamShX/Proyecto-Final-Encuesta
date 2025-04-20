using Domain.Dtos;
using Domain.Models;

namespace Domain.Interfeces
{
    public interface IPreguntaRepository
    {
        Task<IEnumerable<Pregunta>> GetAllByEncuestaIdAsync(int encuestaId);
        Task<Pregunta> GetByIdAsync(int id);
        Task<Pregunta> AddAsync(Pregunta pregunta);
        Task UpdateAsync(Pregunta pregunta);
        Task DeleteAsync(int id);
    }
}
