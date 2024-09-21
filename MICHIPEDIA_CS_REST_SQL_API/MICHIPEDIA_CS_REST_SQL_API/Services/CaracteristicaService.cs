using MICHIPEDIA_CS_REST_SQL_API.Exceptions;
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

        public async Task<CaracteristicaValorada> GetByGuidAsync(Guid caracteristica_guid)
        {
            Caracteristica unaCaracteristica = await _caracteristicaRepository
                .GetByGuidAsync(caracteristica_guid);

            if (unaCaracteristica.Uuid == Guid.Empty)
                throw new AppValidationException($"Caracteristica no encontrada con el guid {caracteristica_guid}");

            CaracteristicaValorada unaCaracteristicaValorada = await _caracteristicaRepository
                .GetDetailedCharacteristicByGuidAsync(caracteristica_guid);

            return unaCaracteristicaValorada;
        }
    }
}
