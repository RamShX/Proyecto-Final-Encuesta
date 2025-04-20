using Domain.Interfeces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Pensistence.Context;

namespace Pensistence.Repositories
{
    public class PreguntaRepository : IPreguntaRepository
    {
        private readonly EncuestaContext _context;

        public PreguntaRepository(EncuestaContext context)
        {
            _context = context;
        }

        public async Task<Pregunta> AddAsync(Pregunta pregunta)
        {
            _context.Preguntas.Add(pregunta);
            await _context.SaveChangesAsync();
            return pregunta;

        }

        public async Task DeleteAsync(int id)
        {
            var pregunta = _context.Preguntas.FindAsync(id);
            if (pregunta != null)
                _context.Preguntas.Remove(pregunta.Result);
                await _context.SaveChangesAsync();

        }

        public async Task<IEnumerable<Pregunta>> GetAllByEncuestaIdAsync(int encuestaId)
        {
            return await _context.Preguntas
                .Include(p => p.OpcionesRespuesta)
                .Where(p => p.EncuestaId == encuestaId)
                .OrderBy(p => p.Orden)
                .ToListAsync();
        }

        public async Task<Pregunta> GetByIdAsync(int id)
        {
            return await _context.Preguntas
                .Include(p => p.OpcionesRespuesta)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public Task<Pregunta> GetByOrden(int orden, int encuestaId)
        {
            return _context.Preguntas
                .FirstOrDefaultAsync(p => p.Orden == orden && p.EncuestaId == encuestaId);
        }

        public async Task UpdateAsync(Pregunta pregunta)
        {
            _context.Entry(pregunta).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
