using Application.Interfeces;
using Domain.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Response;

namespace WebApi.Controllers
{
    [Authorize(Policy = "AdminOnly")]
    [ApiController]
    [Route("api/[controller]")]
    public class PreguntaController : ControllerBase
    {
        private readonly IPreguntaService _preguntaService;

        public PreguntaController(IPreguntaService preguntaService)
        {
            _preguntaService = preguntaService;
        }

        [HttpPost]
        public async Task<IActionResult> CrearPregunta([FromBody] PreguntaCreateDto pregunta)
        {
            if (pregunta == null)
                return BadRequest(new ApiResponse<string>("La pregunta no puede ser null"));

            try
            {
                var preguntaCreada = await _preguntaService.CreateAsync(pregunta);
                return CreatedAtAction(nameof(GetPreguntaById), new { id = preguntaCreada.Id },
                    new ApiResponse<PreguntaDto>(preguntaCreada, "Pregunta creada correctamente"));
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

        [HttpPut]
        public async Task<IActionResult> ActualizarPregunta([FromBody] PreguntaUpdateDto pregunta)
        {
            if(pregunta == null)
                return BadRequest(new ApiResponse<string>("La pregunta no puede ser null"));

            try
            {

                   await _preguntaService.UpdateAsync(pregunta);
                   return Ok(new ApiResponse<string>("Pregunta actualizada correctamente", true));

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

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarPregunta(int id)
        {
            try
            {

                await _preguntaService.DeleteAsync(id);
               
                return Ok(new ApiResponse<string>("Pregunta eliminada correctamente"));
                
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
        public async Task<IActionResult> GetPreguntaById(int id)
        {
            try
            {
                var pregunta = await _preguntaService.GetByIdAsync(id);
             
                return Ok(new ApiResponse<PreguntaDto>(pregunta, "Pregunta obtenida con éxito"));
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
