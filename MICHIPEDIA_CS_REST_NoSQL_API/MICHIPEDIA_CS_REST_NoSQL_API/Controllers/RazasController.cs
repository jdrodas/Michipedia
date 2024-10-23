using MICHIPEDIA_CS_REST_NoSQL_API.Exceptions;
using MICHIPEDIA_CS_REST_NoSQL_API.Models;
using MICHIPEDIA_CS_REST_NoSQL_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace MICHIPEDIA_CS_REST_NoSQL_API.Controllers
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

        [HttpGet("{raza_id:length(24)}")]
        public async Task<IActionResult> GetByIdAsync(string raza_id)
        {
            try
            {
                var unaRaza = await _razaService
                    .GetByIdAsync(raza_id);

                return Ok(unaRaza);
            }
            catch (AppValidationException error)
            {
                return NotFound(error.Message);
            }
        }
    }
}
