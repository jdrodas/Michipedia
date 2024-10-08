using MICHIPEDIA_CS_REST_NoSQL_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace MICHIPEDIA_CS_REST_NoSQL_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResumenController(ResumenService resumenService) : Controller
    {
        private readonly ResumenService _resumenService = resumenService;

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var elResumen = await _resumenService
                .GetAllAsync();

            return Ok(elResumen);
        }
    }
}