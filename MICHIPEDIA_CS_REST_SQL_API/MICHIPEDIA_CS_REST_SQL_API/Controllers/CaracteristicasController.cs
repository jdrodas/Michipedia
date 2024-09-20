using MICHIPEDIA_CS_REST_SQL_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace MICHIPEDIA_CS_REST_SQL_API.Controllers
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
    }
}
