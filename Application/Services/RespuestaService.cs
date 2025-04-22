
using Application.Factory.BaseFactory;
using Application.Factory.Interfeces;
using Application.Factory.Models;
using Application.Interfeces;
using AutoMapper;
using Domain.Dtos;
using Domain.Interfeces;
using Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Application.Services
{
    public class RespuestaService : IRespuestaService
    {
        private readonly IRespuestaRepository _respuestaRepository;
        private readonly IEncuestaRepository _encuestaRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly ILogger<NotificacionEmail> _logger;
        public RespuestaService(IRespuestaRepository respuestaRepository, IEncuestaRepository encuestaRepository,IMapper mapper, IConfiguration confi, ILogger<NotificacionEmail>logger)
        {
            _respuestaRepository = respuestaRepository;
            _encuestaRepository = encuestaRepository;
            _config = confi;
            _logger = logger;
            _mapper = mapper;
        }

        public async  Task<RespuestaEncuestaDto> GetById(int id)
        {
            var respuesta = await _respuestaRepository.GetById(id);
            if (respuesta == null)
                throw new ArgumentException($"La respuesta con ID {id} no existe");

            //Mapear a Dto y devolver
            return _mapper.Map<RespuestaEncuestaDto>(respuesta);
        }

        public async Task<EstadisticasDto> GetEstadisticasByEncuestaId(int encuestaId)
        {
            var encuesta = await _encuestaRepository.GetById(encuestaId);
            if (encuesta == null)
                throw new ArgumentException($"La encuesta con ID {encuestaId} no existe");

            var respuestas = await _respuestaRepository.GetRespuestaByEncuestaId(encuestaId);
            if (respuestas == null || !respuestas.Any())
                throw new ArgumentException($"No se encontraron respuestas para la encuesta con ID {encuestaId}");

            //Crear objeto de estadisticas
            var estadisticas = new EstadisticasDto
            {
                EncuestaId = encuestaId,
                Titulo = encuesta.Titulo,
                TotalRespuestas = respuestas.Count(),
                EstadisticasPorPregunta = new Dictionary<int, PreguntaEstadisticaDto>()
            };

            //Procesar estadisticas para cada pregunta
            foreach (var pregunta in encuesta.Preguntas)
            {
                var respuestasPregunta = await _respuestaRepository.GetRespuestasPreguntaByPreguntaId(pregunta.Id);
                var preguntaStats = new PreguntaEstadisticaDto
                {
                    Pregunta = pregunta.Texto,
                    TipoPregunta = pregunta.TipoPregunta.ToString(),
                    TotalRespuestas = respuestasPregunta.Count(),
                };

                //Procesar estadisticas por tipo de pregunta
                switch (pregunta.TipoPregunta.ToString())
                {
                    case "opcionMultiple":
                        var opcionesStats = new EstadisticaOpcionMultipleDto
                        {
                            Opciones = new Dictionary<string, OpcionEstadisticaDto>()
                        };

                        //agrupar por opcion y calcular frecuencias
                        var agrupado = respuestasPregunta
                            .Where(r => r.OpcionId.HasValue)
                            .GroupBy(r => r.OpcionId.Value)
                            .ToDictionary(g => g.Key, g => g.Count());

                        //calcular frecuencias y porcentajes
                        foreach (var opcion in pregunta.OpcionesRespuesta)
                        {
                            int frecuencia = agrupado.ContainsKey(opcion.Id) ? agrupado[opcion.Id] : 0;
                            double porcentaje = preguntaStats.TotalRespuestas > 0 ? (double)frecuencia / preguntaStats.TotalRespuestas * 100 : 0;

                            opcionesStats.Opciones[opcion.Texto] = new OpcionEstadisticaDto
                            {
                                Frecuencia = frecuencia,
                                Porcentaje = Math.Round(porcentaje, 2)
                            };

                        }

                        preguntaStats.Datos = opcionesStats;

                        break;

                    case "escala":

                        var valores = respuestasPregunta
                            .Where(r => r.ValorEscala.HasValue)
                            .Select(r => r.ValorEscala.Value)
                            .ToList();

                        var escalaStats = new EstadisticasEscalaDto();

                        if(valores.Any())
                        {
                            //calcular promedios
                            escalaStats.Promedio = Math.Round(valores.Average(), 2);

                            //cacular mediana
                            var ordenados = valores.OrderBy(v => v).ToList();
                            int mitad = ordenados.Count /2;

                            if (ordenados.Count % 2 == 0)
                                escalaStats.Mediana = Math.Round((ordenados[mitad - 1] + ordenados[mitad]) / 2.0, 2);
                            else
                                escalaStats.Mediana = ordenados[mitad];

                            //calcular moda
                            escalaStats.Moda = valores
                                .GroupBy(v => v)
                                .OrderByDescending(g => g.Count())
                                .First()
                                .Key;

                        }

                        preguntaStats.Datos = escalaStats;

                        break;
                    default:
                        throw new ArgumentException($"Tipo de pregunta {pregunta.TipoPregunta} no soportado");
                }

                //Agregar estadisticas de pregunta al objeto de estadisticas
                estadisticas.EstadisticasPorPregunta[pregunta.Id] = preguntaStats;
            }

            //Devolver estadisticas
            return estadisticas;

        }

        public async Task<IEnumerable<RespuestaEncuestaDto>> GetRespuestaByEncuestaId(int encuestaId)
        {
            var respuestas = await _respuestaRepository.GetRespuestaByEncuestaId(encuestaId);
            if (respuestas == null || !respuestas.Any())
                throw new ArgumentException($"No se encontraron respuestas para la encuesta con ID {encuestaId}");

            //Mapear a Dto y devolver
            return _mapper.Map<IEnumerable<RespuestaEncuestaDto>>(respuestas);
        }

        public async Task<RespuestaEncuestaDto> GuardarRespuesta(CrearRespuestaEncuestaDto respuesta)
        {
            var encuesta = _encuestaRepository.GetById(respuesta.EncuestaId);
            if (encuesta == null)
                throw new ArgumentException($"La encuesta con ID {respuesta.EncuestaId} no existe");
            

            //validar respuestas
            if(respuesta.RespuestasPreguntas == null || !respuesta.RespuestasPreguntas.Any())
                throw new ArgumentException("Debe incluir al menos una respuesta");

            //creal modelo de respuesta
            var repuesta = new RespuestaEncuesta
            {
                EncuestaId = respuesta.EncuestaId,
                usuarioId = respuesta.UsuariodId ?? 0,
                emailResponde = respuesta.EmailRespondente,
                FechaRespuesta = DateTime.Now,
                RespuestasPreguntas = respuesta.RespuestasPreguntas.Select(rp => new RespuestaPregunta
                {
                    PreguntaId = rp.PreguntaId,
                    OpcionId = rp.OpcionId ?? 0,
                    ValorEscala = rp.ValorEscala ?? 0,
                }).ToList()
            };

            //guardar respuesta
            var respuestaGuardada = _respuestaRepository.GuardarRespuesta(repuesta);
            if (respuestaGuardada == null)
                throw new Exception("Error al guardar la respuesta");

            // un modelo notificacion
            var notificacionAdmin = new CrearNotificacionDto
            {
                Tipo = "EncuestaRespondida",
                Mensaje = $"Un usuario ha respondido la encuesta {respuesta.EncuestaId}"
            };

            //crear notificacion con factory y enviar mensaje
            var notificador = NotificacionFactory.CrearNotificacion("email", _logger, _config);

            await notificador.enviar(notificacionAdmin);


            //Mapear a Dto y devolver
            return _mapper.Map<RespuestaEncuestaDto>(respuestaGuardada);
        }
    }
}
