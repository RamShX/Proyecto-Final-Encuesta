using Domain.Interfeces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Domain.Models;
using Domain.Dtos;
using AutoMapper;

namespace WebApi.Controllers
{
    [Authorize(Policy = "AdminOnly")]
    [ApiController]
    [Route("api/[controller]")]
    public class EncuestaController : ControllerBase
    {
        private readonly IEncuestaRepository _encuestaService;
        private readonly IMapper _mapper;

        public EncuestaController(IEncuestaRepository encuestaService, IMapper mapper)
        {
            _encuestaService = encuestaService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> AddEncuesta([FromBody] CrearEncuestaDto encuestaDto)
        {
            if (encuestaDto == null)
                return BadRequest("La encuesta no puede ser null");

            try
            {
                //mapeamos el dto a la entidad
                var encuesta = _mapper.Map<Encuesta>(encuestaDto);

                var nuevaEncuesta = await _encuestaService.AddEncuesta(encuesta);
                return CreatedAtAction(nameof(GetById), new { id = nuevaEncuesta.Id }, nuevaEncuesta);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocurrió un error inesperado");
            }


        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var encuesta = await _encuestaService.GetById(id);
            if (encuesta == null)
                return NotFound("Encuesta no encontrado");

            try
            {
                return Ok(encuesta);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocurrió un error inesperado");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEncuesta() 
        {
            try
            {
                var encuestas = await _encuestaService.GetAllEncuestas();
                return Ok(encuestas);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocurrió un error inesperado: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEncuesta(int id, [FromBody] Encuesta encuesta)
        {
           
            try
            {

                var actualizado = await _encuestaService.UpdateEncuesta(id, encuesta);

                if (actualizado)
                    return NoContent();
                else
                    return BadRequest("No se pudo actualizar la encuesta");
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocurrió un error inesperado");
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteEncuesta(int id)
        {

            try
            {
                var resultado = await _encuestaService.DeleteEncuesta(id);

                if (resultado)
                    // Respuesta 204 si se eliminó con éxito.
                    return NoContent();
                else
                    return BadRequest("No se pudo eliminar la encuesta");
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocurrió un error inesperado");
            }
        }
    }

}
