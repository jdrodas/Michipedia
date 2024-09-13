using MICHIPEDIA_CS_REST_SQL_API.Interfaces;
using MICHIPEDIA_CS_REST_SQL_API.Models;

namespace MICHIPEDIA_CS_REST_SQL_API.Services
{
    public class ResumenService(IResumenRepository resumenRepository)
    {
        private readonly IResumenRepository _resumenRepository = resumenRepository;

        public async Task<Resumen> GetAllAsync()
        {
            return await _resumenRepository
                .GetAllAsync();
        }
    }
}