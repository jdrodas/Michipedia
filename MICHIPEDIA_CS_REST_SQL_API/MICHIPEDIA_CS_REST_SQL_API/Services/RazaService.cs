using MICHIPEDIA_CS_REST_SQL_API.Exceptions;
using MICHIPEDIA_CS_REST_SQL_API.Interfaces;
using MICHIPEDIA_CS_REST_SQL_API.Models;
using MICHIPEDIA_CS_REST_SQL_API.Repositories;

namespace MICHIPEDIA_CS_REST_SQL_API.Services
{
    public class RazaService(IRazaRepository razaRepository)
    {
        private readonly IRazaRepository _razaRepository = razaRepository;

        public async Task<List<Raza>> GetAllAsync()
        {
            return await _razaRepository
                .GetAllAsync();
        }

        public async Task<Raza> GetByGuidAsync(Guid raza_guid)
        {
            Raza unaRaza = await _razaRepository
                .GetByGuidAsync(raza_guid);

            if (unaRaza.Uuid == Guid.Empty)
                throw new AppValidationException($"Raza no encontrada con el guid {raza_guid}");

            return unaRaza;
        }
    }
}
