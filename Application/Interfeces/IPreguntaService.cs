

using Domain.Dtos;

namespace Application.Interfeces
{
    public interface IPreguntaService
    {
        Task<PreguntaDto> GetByIdAsync(int id);
        Task<PreguntaDto> CreateAsync(PreguntaCreateDto preguntaDto);
        Task UpdateAsync(PreguntaUpdateDto preguntaDto);
        Task DeleteAsync(int id);
    }
}
