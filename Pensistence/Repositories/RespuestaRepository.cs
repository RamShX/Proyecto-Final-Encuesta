

using Domain.Interfeces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Pensistence.Context;

namespace Pensistence.Repositories
{
    public class RespuestaRepository : IRespuestaRepository
    {
        private readonly EncuestaContext _context;
        public RespuestaRepository(EncuestaContext context)
        {
            _context = context;
        }

        public async Task<bool> ExisteRespuestaUsuario(int encuestaId, int usuarioId)
        {
            return await _context.RespuestasEncuestas
                .AnyAsync(r => r.EncuestaId == encuestaId && r.usuarioId == usuarioId);
        }

        public async Task<RespuestaEncuesta> GetById(int id)
        {
            return await _context.RespuestasEncuestas
                .Include(r => r.RespuestasPreguntas)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<IEnumerable<RespuestaEncuesta>> GetRespuestaByEncuestaId(int encuestaId)
        {
            return await _context.RespuestasEncuestas
                .Include(r => r.RespuestasPreguntas)
                .Where(r => r.EncuestaId == encuestaId)
                .ToListAsync();
        }

        public async Task<IEnumerable<RespuestaPregunta>> GetRespuestasPreguntaByPreguntaId(int preguntaId)
        {
            return await _context.RespuestasPreguntas
                .Where(rp => rp.PreguntaId == preguntaId)
                .ToListAsync();
        }

        public async  Task<RespuestaEncuesta> GuardarRespuesta(RespuestaEncuesta respuesta)
        {
            _context.RespuestasEncuestas.Add(respuesta);
            await _context.SaveChangesAsync();
            return respuesta;
        }
    } 
   
    
}
