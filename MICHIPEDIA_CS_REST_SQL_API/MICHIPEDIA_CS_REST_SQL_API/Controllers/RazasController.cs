using MICHIPEDIA_CS_REST_SQL_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace MICHIPEDIA_CS_REST_SQL_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RazasController(RazaService razaService) : Controller
    {
        private readonly RazaService _razaService = razaService;

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var lasRazas = await _razaService
                .GetAllAsync();

            return Ok(lasRazas);
        }
    }
}
