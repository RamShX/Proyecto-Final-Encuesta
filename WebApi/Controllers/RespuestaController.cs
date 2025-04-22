using Application.Interfeces;
using Domain.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Response;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RespuestaController : ControllerBase
    {
        private readonly IRespuestaService _respuestaService;

        public RespuestaController(IRespuestaService respuestaService)
        {
            _respuestaService = respuestaService;
        }

        [HttpPost("encuestas/{id}/responder")]
        [AllowAnonymous]
        public async Task<IActionResult> ResponderEncuesta(int id, [FromBody] CrearRespuestaEncuestaDto respuestaDto)
        {
            if (respuestaDto == null)
                return BadRequest(new ApiResponse<string>("La respuesta no puede ser null"));

            try
            {
                //asegurarse que el id coinside con el de la respuesta
                respuestaDto.EncuestaId = id;

                var respuestaCreada = await _respuestaService.GuardarRespuesta(respuestaDto);

                return CreatedAtAction(nameof(GetRespuestaByEncuestaId), new { id = respuestaCreada.Id },
                    new ApiResponse<RespuestaEncuestaDto>(respuestaCreada, "Encuesta respondida correctamente"));
            }
            catch (ArgumentException e)
            {
                return BadRequest(new ApiResponse<string>(e.Message));
            }
            catch (Exception e)
            {
                return StatusCode(500, new ApiResponse<string>($"Ocurrió un error inesperado: {e.Message}"));
            }
        }

        [HttpGet("encuestas/{id}/respuestas")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> GetRespuestaByEncuestaId(int id)
        {
            try
            {
                var respuestas = await _respuestaService.GetRespuestaByEncuestaId(id);

                return Ok(new ApiResponse<IEnumerable<RespuestaEncuestaDto>>(respuestas, "Respuestas obtenidas con éxito"));
            }
            catch (ArgumentException e)
            {
                return BadRequest(new ApiResponse<string>(e.Message));
            }
            catch (Exception e)
            {
                return StatusCode(500, new ApiResponse<string>($"Ocurrió un error inesperado: {e.Message}"));
            }
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> GetResultaById(int id)
        {
            try
            {
                var respuesta = await _respuestaService.GetById(id);

                return Ok(new ApiResponse<RespuestaEncuestaDto>(respuesta, "Respuesta obtenida con éxito"));
            }
            catch (ArgumentException e)
            {
                return BadRequest(new ApiResponse<string>(e.Message));
            }
            catch (Exception e)
            {
                return StatusCode(500, new ApiResponse<string>($"Ocurrió un error inesperado: {e.Message}"));
            }
        }


        [HttpGet("encuestas/{id}/estadisticas")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> GetEstadisticasByEncuestaId(int id) 
        {
            try
            {
                var estadisticas = await _respuestaService.GetEstadisticasByEncuestaId(id);
                return Ok(new ApiResponse<EstadisticasDto>(estadisticas, "Estadísticas obtenidas con éxito"));

            }
            catch (ArgumentException e)
            {
                return BadRequest(new ApiResponse<string>(e.Message));
            }
            catch (Exception e)
            {
                return StatusCode(500, new ApiResponse<string>($"Ocurrió un error inesperado: {e.Message}"));
            }
        }

    }
}
