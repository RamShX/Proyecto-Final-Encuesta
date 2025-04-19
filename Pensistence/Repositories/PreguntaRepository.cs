

using Domain.Dtos;
using Domain.Interfeces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Pensistence.Context;

namespace Pensistence.Repositories
{
    public class PreguntaRepository : IPreguntaService
    {
        private readonly EncuestaContext _context;
        public PreguntaRepository(EncuestaContext context)
        {
            _context = context;
        }

        public async Task<bool> ActualizarPregunta(int id,PreguntaDto pregunta)
        {

            var entity = await _context.Preguntas.FindAsync(id);

            if (entity == null)
                 return false;

            entity.Texto = pregunta.Texto;
            entity.TipoPregunta = pregunta.TipoPregunta;
            entity.Orden = pregunta.Orden;
            entity.Obligatorio = pregunta.Obligatorio;
            entity.EncuestaId = pregunta.EncuestaId;
;
            await _context.SaveChangesAsync();
            return true;


        }

        public async Task<int> CrearPregunta(PreguntaDto pregunta)
        {
            var entity = new Pregunta
            {
                Texto = pregunta.Texto,
                TipoPregunta = pregunta.TipoPregunta,
                Orden = pregunta.Orden,
                Obligatorio = pregunta.Obligatorio,
                EncuestaId = pregunta.EncuestaId
            };

            _context.Preguntas.Add(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public async  Task<bool> EliminarPregunta(int id)
        {
            var pregunta = await _context.Preguntas.FindAsync(id);

            if (pregunta == null)
                return false;

            _context.Preguntas.Remove(pregunta);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<PreguntaDto>> GetPreguntasByEncuestaId(int encuestaId)
        {
            var preguntas = await _context.Preguntas
                .Where(p => p.EncuestaId == encuestaId)
                .ToListAsync();

            return preguntas.Select(p => new PreguntaDto
            {
                Texto = p.Texto,
                TipoPregunta = p.TipoPregunta,
                Orden = p.Orden,
                Obligatorio = p.Obligatorio,
                EncuestaId = p.EncuestaId
            }).ToList();
        }
    }
}
