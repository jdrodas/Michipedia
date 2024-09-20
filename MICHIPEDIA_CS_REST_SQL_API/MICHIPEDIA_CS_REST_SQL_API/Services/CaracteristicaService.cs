using MICHIPEDIA_CS_REST_SQL_API.Interfaces;
using MICHIPEDIA_CS_REST_SQL_API.Models;

namespace MICHIPEDIA_CS_REST_SQL_API.Services
{
    public class CaracteristicaService(ICaracteristicaRepository caracteristicaRepository)
    {
        private readonly ICaracteristicaRepository _caracteristicaRepository = caracteristicaRepository;

        public async Task<List<Caracteristica>> GetAllAsync()
        {
            return await _caracteristicaRepository
                .GetAllAsync();
        }
    }
}
