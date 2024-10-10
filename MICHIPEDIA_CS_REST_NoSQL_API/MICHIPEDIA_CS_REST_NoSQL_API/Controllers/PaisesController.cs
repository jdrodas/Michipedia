using MICHIPEDIA_CS_REST_NoSQL_API.Exceptions;
using MICHIPEDIA_CS_REST_NoSQL_API.Models;
using MICHIPEDIA_CS_REST_NoSQL_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace MICHIPEDIA_CS_REST_NoSQL_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaisesController(PaisService paisService) : Controller
    {
        private readonly PaisService _paisService = paisService;

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var losPaises = await _paisService
                .GetAllAsync();

            return Ok(losPaises);
        }

        [HttpGet("{pais_id:length(24)}")]
        public async Task<IActionResult> GetByIdAsync(string pais_id)
        {
            try
            {
                var unPais = await _paisService
                    .GetByIdAsync(pais_id);

                return Ok(unPais);
            }
            catch (AppValidationException error)
            {
                return NotFound(error.Message);
            }
        }

        //[HttpGet("{pais_guid:Guid}/Razas")]
        //public async Task<IActionResult> GetBreedsAsync(Guid pais_guid)
        //{
        //    try
        //    {
        //        var lasRazasAsociadas = await _paisService
        //            .GetBreedsAsync(pais_guid);

        //        return Ok(lasRazasAsociadas);
        //    }
        //    catch (AppValidationException error)
        //    {
        //        return NotFound(error.Message);
        //    }
        //}

        [HttpPost]
        public async Task<IActionResult> CreateAsync(Pais unPais)
        {
            try
            {
                var paisCreado = await _paisService
                    .CreateAsync(unPais);

                return Ok(paisCreado);
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

        //[HttpPut]
        //public async Task<IActionResult> UpdateAsync(Pais unPais)
        //{
        //    try
        //    {
        //        var paisActualizado = await _paisService
        //            .UpdateAsync(unPais);

        //        return Ok(unPais);
        //    }
        //    catch (AppValidationException error)
        //    {
        //        return BadRequest($"Error de validación: {error.Message}");
        //    }
        //    catch (DbOperationException error)
        //    {
        //        return BadRequest($"Error de operacion en DB: {error.Message}");
        //    }
        //}

        //[HttpDelete]
        //public async Task<IActionResult> RemoveAsync(Guid pais_guid)
        //{
        //    try
        //    {
        //        var paisEliminado = await _paisService
        //            .RemoveAsync(pais_guid);

        //        return Ok(paisEliminado);
        //    }
        //    catch (AppValidationException error)
        //    {
        //        return BadRequest($"Error de validación: {error.Message}");
        //    }
        //    catch (DbOperationException error)
        //    {
        //        return BadRequest($"Error de operacion en DB: {error.Message}");
        //    }
        //}
    }
}
