using Domain.Interfeces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Domain.Models;
using Domain.Dtos;
using AutoMapper;
using WebApi.Response;

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
                return BadRequest(new ApiResponse<string>("La encuesta no puede ser null"));

            try
            {
                //mapeamos el dto a la entidad
                var encuesta = _mapper.Map<Encuesta>(encuestaDto);

                encuesta.CreadoEn = DateTime.UtcNow;
                encuesta.ActualizadoEn = DateTime.UtcNow;

                var nuevaEncuesta = await _encuestaService.AddEncuesta(encuesta);
                return CreatedAtAction(nameof(GetById), new { id = nuevaEncuesta.Id }, 
                    new ApiResponse<Encuesta>(encuesta, "Encuesta creada correctamente"));
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
        public async Task<IActionResult> GetById(int id)
        {
            var encuesta = await _encuestaService.GetById(id);
            if (encuesta == null)
                return NotFound(new ApiResponse<string>("Encuesta no encontrado"));

            try
            {
                var encuestaDto = _mapper.Map<EncuestaRespuestaDto>(encuesta);

                return Ok(new ApiResponse<EncuestaRespuestaDto>(encuestaDto, "Encuesta obtenida con éxito"));
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

        [HttpGet]
        public async Task<IActionResult> GetAllEncuesta() 
        {
            try
            {
                var encuestas = await _encuestaService.GetAllEncuestas();
                var encuestaDto = _mapper.Map<IEnumerable<EncuestaRespuestaDto>>(encuestas);
                return Ok(new ApiResponse<IEnumerable<EncuestaRespuestaDto>>(encuestaDto, "Encuestas obtenidas con éxito"));
            }
            catch (ArgumentException e)
            {
                return BadRequest(new ApiResponse<string>(e.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<string>($"Ocurrió un error inesperado: {ex.Message}"));
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEncuesta(int id, [FromBody] EncuestaUpdateDto encuestaDto)
        {
           
            try
            {
                var encuesta = _mapper.Map<Encuesta>(encuestaDto);

                var actualizado = await _encuestaService.UpdateEncuesta(id, encuesta);

                if (actualizado)
                    return Ok(new ApiResponse<string>( "Encuesta actualizada correctamente", true));
                else
                    return BadRequest(new ApiResponse<string>("No se pudo actualizar la encuesta"));

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

        [HttpDelete]
        public async Task<IActionResult> DeleteEncuesta(int id)
        {

            try
            {
                var resultado = await _encuestaService.DeleteEncuesta(id);


                if (resultado)
                    return Ok(new ApiResponse<string>("Encuesta eliminada correctamente"));
                else
                    return BadRequest(new ApiResponse<string>("No se pudo eliminar la encuesta"));
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
