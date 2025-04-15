
using Domain.Models;

namespace Domain.Interfeces
{
    public interface IEncuestaRepository
    {
        Task<IEnumerable<Encuesta>> GetAllEncuestas();
        Task<Encuesta> AddEncuesta(Encuesta entity);
        Task<Encuesta> GetById(int id);
        Task<bool> ExisteEncuesta(int id);
        Task<bool> UpdateEncuesta(int id, Encuesta entity);
        Task<bool> DeleteEncuesta(int id);
        Task<bool> EncuestaEsPublica(int id);
    }
}
