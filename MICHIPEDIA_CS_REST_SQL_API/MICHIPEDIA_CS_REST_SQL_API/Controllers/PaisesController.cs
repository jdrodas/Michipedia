using MICHIPEDIA_CS_REST_SQL_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace MICHIPEDIA_CS_REST_SQL_API.Controllers
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
    }
}
