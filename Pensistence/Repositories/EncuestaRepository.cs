
using Domain.Interfeces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Pensistence.Context;

namespace Pensistence.Repositories
{
    public class EncuestaRepository : IEncuestaRepository
    {
        private readonly EncuestaContext _context;
        public EncuestaRepository(EncuestaContext context)
        {
            _context = context;
        }

        public async Task<Encuesta> AddEncuesta(Encuesta entity)
        {
            _context.Encuestas.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteEncuesta(int id)
        {
            var encuesta = await _context.Encuestas.FindAsync(id);
            if (encuesta == null)
            {
                return false;
            }
            _context.Encuestas.Remove(encuesta);
            await _context.SaveChangesAsync();
            return true;


        }

        public async Task<bool> EncuestaEsPublica(int id)
        {
            var encuesta = await _context.Encuestas.FindAsync(id);

            return encuesta?.EsPublica ?? false;
        }

        public async Task<bool> ExisteEncuesta(int id)
        {
            var encuesta = await  _context.Encuestas.FindAsync(id);

            return encuesta != null;
        }

        public async Task<IEnumerable<Encuesta>> GetAllEncuestas()
        {
            var encuestas = await _context.Encuestas
                .Include(e => e.Creador)
                .ToListAsync();

            return encuestas;
        }

        public async Task<Encuesta> GetById(int id)
        {
            var encuesta = await _context.Encuestas
                .Include(e => e.Creador)
                .FirstOrDefaultAsync(e => e.Id == id);

            //if (encuesta == null)
            //{
            //    throw new KeyNotFoundException("La encuesta no existe");
            //}

            return encuesta;
        }

        public async Task<bool> UpdateEncuesta(int id, Encuesta entity)
        {
            var encuesta = await _context.Encuestas.FindAsync(id);

            if (encuesta == null)
                return false;


            encuesta.Titulo = entity.Titulo;
            encuesta.Descripcion = entity.Descripcion;
            encuesta.EsPublica = entity.EsPublica;
            encuesta.EnlacePublico = entity.EnlacePublico;
            encuesta.FechaInicio = entity.FechaInicio;
            encuesta.FechaExpiracion = entity.FechaExpiracion;
            encuesta.Activo = entity.Activo;
            
            _context.Encuestas.Update(encuesta);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
