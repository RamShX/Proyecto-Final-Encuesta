using Application.Interfeces;
using Domain.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Response;

namespace WebApi.Controllers
{
    //[Authorize(Policy = "AdminOnly")]
    //[ApiController]
    //[Route("api/[controller]")]
    //public class PreguntaController : ControllerBase
    //{
    //    private readonly IPreguntaService _preguntaService;

    //    public PreguntaController(IPreguntaService preguntaService)
    //    {
    //        _preguntaService = preguntaService;
    //    }

    //    [HttpPost]
    //    public async Task<IActionResult> CrearPregunta([FromBody] PreguntaDto pregunta)
    //    {
    //        if (pregunta == null)
    //            return BadRequest(new ApiResponse<string>("La pregunta no puede ser null"));

    //        try
    //        {
    //            var id = await _preguntaService.CrearPregunta(pregunta);
    //            return CreatedAtAction(nameof(GetPreguntaById), new { id = id },
    //                new ApiResponse<int>(id, "Pregunta creada correctamente"));
    //        }
    //        catch (ArgumentException e)
    //        {
    //            return BadRequest(new ApiResponse<string>(e.Message));
    //        }
    //        catch (Exception e)
    //        {
    //            return StatusCode(500, new ApiResponse<string>($"Ocurrió un error inesperado: {e.Message}"));
    //        }
    //    }

    //    [HttpPut("{id}")]
    //    public async Task<IActionResult> ActualizarPregunta(int id, [FromBody] PreguntaDto pregunta)
    //    {
    //        try
    //        {
    //            var actualizado = await _preguntaService.ActualizarPregunta(id, pregunta);
    //            if (actualizado)
    //                return Ok(new ApiResponse<string>("Pregunta actualizada correctamente", true));
    //            else
    //                return BadRequest(new ApiResponse<string>("No se pudo actualizar la pregunta"));
    //        }
    //        catch (ArgumentException e)
    //        {
    //            return BadRequest(new ApiResponse<string>(e.Message));
    //        }
    //        catch (Exception e)
    //        {
    //            return StatusCode(500, new ApiResponse<string>($"Ocurrió un error inesperado: {e.Message}"));
    //        }
    //    }

    //    [HttpDelete("{id}")]
    //    public async Task<IActionResult> EliminarPregunta(int id)
    //    {
    //        try
    //        {
    //            var eliminado = await _preguntaService.EliminarPregunta(id);
    //            if (eliminado)
    //                return Ok(new ApiResponse<string>("Pregunta eliminada correctamente"));
    //            else
    //                return BadRequest(new ApiResponse<string>("No se pudo eliminar la pregunta"));
    //        }
    //        catch (ArgumentException e)
    //        {
    //            return BadRequest(new ApiResponse<string>(e.Message));
    //        }
    //        catch (Exception e)
    //        {
    //            return StatusCode(500, new ApiResponse<string>($"Ocurrió un error inesperado: {e.Message}"));
    //        }
    //    }

    //    [HttpGet("{id}")]
    //    public async Task<IActionResult> GetPreguntaById(int id)
    //    {
    //        try
    //        {
    //            var pregunta = await _preguntaService.GetPreguntaById(id);
    //            if (pregunta == 0) // Esto depende de si GetPreguntaById devuelve el DTO o solo el ID.
    //                return NotFound(new ApiResponse<string>("Pregunta no encontrada"));

    //            return Ok(new ApiResponse<int>(pregunta, "Pregunta obtenida con éxito"));
    //        }
    //        catch (ArgumentException e)
    //        {
    //            return BadRequest(new ApiResponse<string>(e.Message));
    //        }
    //        catch (Exception e)
    //        {
    //            return StatusCode(500, new ApiResponse<string>($"Ocurrió un error inesperado: {e.Message}"));
    //        }
    //    }

    //    [HttpGet("por-encuesta/{encuestaId}")]
    //    public async Task<IActionResult> GetPreguntasByEncuestaId(int encuestaId)
    //    {
    //        try
    //        {
    //            var preguntas = await _preguntaService.GetPreguntasByEncuestaId(encuestaId);
    //            return Ok(new ApiResponse<List<PreguntaDto>>(preguntas, "Preguntas obtenidas correctamente"));
    //        }
    //        catch (ArgumentException e)
    //        {
    //            return BadRequest(new ApiResponse<string>(e.Message));
    //        }
    //        catch (Exception e)
    //        {
    //            return StatusCode(500, new ApiResponse<string>($"Ocurrió un error inesperado: {e.Message}"));
    //        }
    //    }
    //}
    
}
