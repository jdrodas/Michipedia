using MICHIPEDIA_CS_REST_NoSQL_API.Exceptions;
using MICHIPEDIA_CS_REST_NoSQL_API.Models;
using MICHIPEDIA_CS_REST_NoSQL_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace MICHIPEDIA_CS_REST_NoSQL_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComportamientosController(ComportamientoService comportamientoService) : Controller
    {
        private readonly ComportamientoService _comportamientoService = comportamientoService;

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var losComportamientos = await _comportamientoService
                .GetAllAsync();

            return Ok(losComportamientos);
        }

        [HttpGet("{comportamiento_id:length(24)}")]
        public async Task<IActionResult> GetByIdAsync(string comportamiento_id)
        {
            try
            {
                var unComportamiento = await _comportamientoService
                    .GetByIdAsync(comportamiento_id);

                return Ok(unComportamiento);
            }
            catch (AppValidationException error)
            {
                return NotFound(error.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(Comportamiento unComportamiento)
        {
            try
            {
                var comportamientoCreado = await _comportamientoService
                    .CreateAsync(unComportamiento);

                return Ok(comportamientoCreado);
            }
            catch (AppValidationException error)
            {
                return BadRequest($"Error en la validación: {error.Message}");
            }
            catch (DbOperationException error)
            {
                return BadRequest($"Error en la operación de la DB {error.Message}");
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(Comportamiento unComportamiento)
        {
            try
            {
                var comportamientoActualizado = await _comportamientoService
                    .UpdateAsync(unComportamiento);

                return Ok(unComportamiento);
            }
            catch (AppValidationException error)
            {
                return BadRequest($"Error de validación: {error.Message}");
            }
            catch (DbOperationException error)
            {
                return BadRequest($"Error de operacion en DB: {error.Message}");
            }
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveAsync(string comportamiento_id)
        {
            try
            {
                var comportamientoEliminado = await _comportamientoService
                    .RemoveAsync(comportamiento_id);

                return Ok(comportamientoEliminado);
            }
            catch (AppValidationException error)
            {
                return BadRequest($"Error de validación: {error.Message}");
            }
            catch (DbOperationException error)
            {
                return BadRequest($"Error de operacion en DB: {error.Message}");
            }
        }
    }
}

