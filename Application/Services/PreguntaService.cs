
using Application.Interfeces;
using AutoMapper;
using Domain.Dtos;
using Domain.Enum;
using Domain.Interfeces;
using Domain.Models;

namespace Application.Services
{
    public class PreguntaService : IPreguntaService
    {
        private readonly IPreguntaRepository _preguntaRepository;
        private readonly IEncuestaRepository _encuestaRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;

        public PreguntaService(IPreguntaRepository preguntaRepository, IEncuestaRepository encuestaRepository, IUsuarioRepository usuarioRepository, IMapper mapper)
        {
            _preguntaRepository = preguntaRepository;
            _encuestaRepository = encuestaRepository;
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
        }

        public async Task<PreguntaDto> CreateAsync(PreguntaCreateDto preguntaDto)
        {
            //Validar si eciste la encuesta
            var encuesta = await _encuestaRepository.GetById(preguntaDto.EncuestaId);
            if (encuesta == null)
                throw new ArgumentException("Encuesta no encontrada");


            var pregunta = new Pregunta
            {
                EncuestaId = preguntaDto.EncuestaId,
                Texto = preguntaDto.Texto,
                TipoPregunta = preguntaDto.TipoPregunta,
                Orden = preguntaDto.Orden,
                Obligatorio = preguntaDto.Obligatorio,
                CreadoEn = DateTime.UtcNow,
                ActualizadoEn = DateTime.UtcNow,
            };

            //Agregar las opciones de respuesta
            if (preguntaDto.OpcionesRespuesta != null && preguntaDto.OpcionesRespuesta.Count > 0)
            {
                foreach (var opcion in preguntaDto.OpcionesRespuesta)
                {
                    var opcionRespuesta = new OpcionRespuesta
                    {
                        Texto = opcion.Texto,
                        Valor = opcion.Valor,
                        Orden = opcion.Orden,
                        CreadoEn = DateTime.UtcNow,
                        ActualizadoEn = DateTime.UtcNow,
                    };
                    pregunta.OpcionesRespuesta.Add(opcionRespuesta);
                }
            }


            var preguntaCreada = await _preguntaRepository.AddAsync(pregunta);
            return _mapper.Map<PreguntaDto>(preguntaCreada);
        }

        public async Task DeleteAsync(int id)
        {
            var pregunta = await _preguntaRepository.GetByIdAsync(id);
            if (pregunta == null)
                throw new ArgumentException($"Pregunta con Id {id} no encontrada");

            await _preguntaRepository.DeleteAsync(id);

        }

        public async Task<PreguntaDto> GetByIdAsync(int id)
        {
            var pregunta = await _preguntaRepository.GetByIdAsync(id);
            if (pregunta == null)
                throw new ArgumentException("Pregunta no encontrada");

            return _mapper.Map<PreguntaDto>(pregunta);
        }

        public async Task UpdateAsync(PreguntaUpdateDto preguntaDto)
        {
            var preguntaExistente = await _preguntaRepository.GetByIdAsync(preguntaDto.Id);
            if (preguntaExistente == null)
                throw new ArgumentException($"Pregunta con Id {preguntaDto.Id} no encontrada");

            // Actualizar propiedades
            preguntaExistente.Texto = preguntaDto.Texto;
            preguntaExistente.TipoPregunta = preguntaDto.TipoPregunta;
            preguntaExistente.Orden = preguntaDto.Orden;
            preguntaExistente.Obligatorio = preguntaDto.Obligatoria;
            preguntaExistente.ActualizadoEn = DateTime.Now;

            // Actualizar opciones de respuesta si se proporcionan
     
            if (preguntaDto.OpcionesRespuesta != null)
            {
                // Borrar opciones existentes que no están en el DTO
                var opcionesExistentes = preguntaExistente.OpcionesRespuesta.ToList();
                var opcionesAMantener = preguntaDto.OpcionesRespuesta
                    .Where(o => o.Id > 0)
                    .Select(o => o.Id)
                    .ToList();

                var opcionesABorrar = opcionesExistentes
                    .Where(o => !opcionesAMantener.Contains(o.Id))
                    .ToList();

                foreach (var opcion in opcionesABorrar)
                {
                    preguntaExistente.OpcionesRespuesta.Remove(opcion);
                }

                // Actualizar opciones existentes
                foreach (var opcionDto in preguntaDto.OpcionesRespuesta.Where(o => o.Id > 0))
                {
                    var opcion = opcionesExistentes.FirstOrDefault(o => o.Id == opcionDto.Id);
                    if (opcion != null)
                    {
                        opcion.Texto = opcionDto.Texto;
                        opcion.Valor = opcionDto.Valor ?? 0;
                        opcion.Orden = opcionDto.Orden;
                        opcion.ActualizadoEn = DateTime.Now;
                    }
                }

                // Agregar nuevas opciones
                foreach (var opcionDto in preguntaDto.OpcionesRespuesta.Where(o => o.Id == 0))
                {
                    preguntaExistente.OpcionesRespuesta.Add(new OpcionRespuesta
                    {
                        Texto = opcionDto.Texto,
                        Valor = opcionDto.Valor ?? 0,
                        Orden = opcionDto.Orden,
                        CreadoEn = DateTime.Now,
                        ActualizadoEn = DateTime.Now
                    });
                }
            }

            await _preguntaRepository.UpdateAsync(preguntaExistente);


        }
    }
}
