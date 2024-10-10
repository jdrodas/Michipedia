using MICHIPEDIA_CS_REST_NoSQL_API.Exceptions;
using MICHIPEDIA_CS_REST_NoSQL_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace MICHIPEDIA_CS_REST_NoSQL_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CaracteristicasController(CaracteristicaService caracteristicaService) : Controller
    {
        private readonly CaracteristicaService _caracteristicaService = caracteristicaService;

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var lasCaracteristicas = await _caracteristicaService
                .GetAllAsync();

            return Ok(lasCaracteristicas);
        }

        [HttpGet("{caracteristica_id:length(24)}")]
        public async Task<IActionResult> GetByIdAsync(string caracteristica_id)
        {
            try
            {
                var unaCaracteristica = await _caracteristicaService
                    .GetByIdAsync(caracteristica_id);

                return Ok(unaCaracteristica);
            }
            catch (AppValidationException error)
            {
                return NotFound(error.Message);
            }
        }
    }
}
