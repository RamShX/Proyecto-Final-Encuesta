
using Domain.Dtos;
using Domain.Enum;
using Domain.Interfeces;

namespace Application.Services
{
    public class PreguntaService : IPreguntaService
    {
        private readonly IPreguntaService _preguntaRepository;
        public PreguntaService(IPreguntaService preguntaRepository)
        {
            _preguntaRepository = preguntaRepository;
        }

        public Task<bool> ActualizarPregunta(int id, PreguntaDto pregunta)
        {
            // Validar si la pregunta existe
            var preguntaExistente = _preguntaRepository.GetPreguntasByEncuestaId(id);
            if (preguntaExistente == null)
                throw new ArgumentException("La pregunta no existe");

            // Validar si el texto de la pregunta es nulo o vacio
            if (string.IsNullOrEmpty(pregunta.Texto))
                throw new ArgumentException("El texto de la pregunta no puede ser nulo o vacio");

            // Validar si el tipo de pregunta es valido
            if (pregunta.TipoPregunta != TipoPregunta.opcionMultiple && pregunta.TipoPregunta != TipoPregunta.escala)
                throw new ArgumentException("El tipo de pregunta no es valido");

            // Validar si el orden de la pregunta es menor a 0
            if (pregunta.Orden < 0)
                throw new ArgumentException("El orden de la pregunta no puede ser menor a 0");

            return _preguntaRepository.ActualizarPregunta(id, pregunta);
        }

        public Task<int> CrearPregunta(PreguntaDto pregunta)
        {
            // Validar si la encuesta existe
            var encuesta = _preguntaRepository.GetPreguntasByEncuestaId(pregunta.EncuestaId);
            if (encuesta == null)
                throw new ArgumentException("La encuesta no existe");

            // Validar si el texto de la pregunta es nulo o vacio
            if (string.IsNullOrEmpty(pregunta.Texto))
                throw new ArgumentException("El texto de la pregunta no puede ser nulo o vacio");
            // Validar si el tipo de pregunta es valido
            if (pregunta.TipoPregunta != TipoPregunta.opcionMultiple && pregunta.TipoPregunta != TipoPregunta.escala)
                throw new ArgumentException("El tipo de pregunta no es valido");
            // Validar si el orden de la pregunta es menor a 0
            if (pregunta.Orden < 0)
                throw new ArgumentException("El orden de la pregunta no puede ser menor a 0");
    

            return _preguntaRepository.CrearPregunta(pregunta);
        }

        public Task<bool> EliminarPregunta(int id)
        {
            // Validar si la pregunta existe
            var pregunta = _preguntaRepository.GetPreguntasByEncuestaId(id);

            if (pregunta == null)
                throw new ArgumentException("La pregunta no existe");

            return _preguntaRepository.EliminarPregunta(id);
        }

        public Task<List<PreguntaDto>> GetPreguntasByEncuestaId(int encuestaId)
        {
            // Validar si la encuesta existe
            var encuesta = _preguntaRepository.GetPreguntasByEncuestaId(encuestaId);
            if (encuesta == null)
                throw new ArgumentException("La encuesta no existe");

            return _preguntaRepository.GetPreguntasByEncuestaId(encuestaId);
        }
    }
}
