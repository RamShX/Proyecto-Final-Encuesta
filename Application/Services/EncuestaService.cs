
using Domain.Interfeces;
using Domain.Models;

namespace Application.Services
{
    public class EncuestaService : IEncuestaRepository
    {
        private readonly IEncuestaRepository _encuestaRepository;

        public EncuestaService(IEncuestaRepository encuestaRepository)
        {
            _encuestaRepository = encuestaRepository;
        }

        public async Task<Encuesta> AddEncuesta(Encuesta entity)
        {
            // Validar si el creador de la encuesta existe
            var creador = await _encuestaRepository.ExisteEncuesta(entity.UsuarioId);
            if (!creador)
                throw new ArgumentException("El creador de la encuesta no existe");


            // Validar si la fecha de inicio es menor a la fecha de expiracion
            if (entity.FechaInicio >= entity.FechaExpiracion)
                throw new ArgumentException("La fecha de inicio no puede ser mayor o igual a la fecha de expiracion");

            // mapeo
            //var encuesta = new Encuesta
            //{
            //    Titulo = entity.Titulo,
            //    Descripcion = entity.Descripcion,
            //    EsPublica = entity.EsPublica,
            //    EnlacePublico = entity.EnlacePublico,
            //    FechaInicio = entity.FechaInicio,
            //    FechaExpiracion = entity.FechaExpiracion,
            //    CreadoEn = DateTime.UtcNow,
            //    ActualizadoEn = DateTime.UtcNow,
            //    Activo = true,
            //    UsuarioId = entity.UsuarioId
            //};

            return await _encuestaRepository.AddEncuesta(entity);
        }

        public async Task<bool> DeleteEncuesta(int id)
        {
            var encuesta = await _encuestaRepository.GetById(id);
            if (encuesta == null)
                throw new ArgumentException("La encuesta no existe");

            return await _encuestaRepository.DeleteEncuesta(id);
        }

        public async Task<bool> EncuestaEsPublica(int id)
        {
            var encuesta = await _encuestaRepository.EncuestaEsPublica(id);
            if (encuesta == null)
            {
                throw new ArgumentException("La encuesta no existe");
            }
            return encuesta;
        }

        public async Task<bool> ExisteEncuesta(int id)
        {
            var encuesta = await _encuestaRepository.ExisteEncuesta(id);
            if (!encuesta)
                throw new ArgumentException("La encuesta no existe");

            return encuesta;
        }

        public async  Task<IEnumerable<Encuesta>> GetAllEncuestas()
        {
            var encuestas = await _encuestaRepository.GetAllEncuestas();

            if (encuestas == null || !encuestas.Any())
                throw new ArgumentException("No hay encuestas disponibles");

            return encuestas;
        }

        public async  Task<Encuesta> GetById(int id)
        {
            var encuesta = await _encuestaRepository.GetById(id);
            if (encuesta == null)
            {
                throw new ArgumentException("La encuesta no existe");
            }
            return encuesta;
        }

        public async Task<bool> UpdateEncuesta(int id, Encuesta entity)
        {
            var encuesta = await _encuestaRepository.GetById(id);

            if (encuesta == null || encuesta.Id != id)
                throw new ArgumentException("La encuesta es inválida o el ID no coincide");

            // Validar si la fecha de inicio es menor a la fecha de expiracion
            if (entity.FechaInicio >= entity.FechaExpiracion)
                throw new ArgumentException("La fecha de inicio no puede ser mayor o igual a la fecha de expiracion");

            return await _encuestaRepository.UpdateEncuesta(id, entity);
        }
    }
}
