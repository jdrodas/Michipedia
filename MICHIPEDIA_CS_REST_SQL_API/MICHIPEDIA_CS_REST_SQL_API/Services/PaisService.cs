using MICHIPEDIA_CS_REST_SQL_API.Exceptions;
using MICHIPEDIA_CS_REST_SQL_API.Interfaces;
using MICHIPEDIA_CS_REST_SQL_API.Models;

namespace MICHIPEDIA_CS_REST_SQL_API.Services
{
    public class PaisService(IPaisRepository paisRepository,
                             IRazaRepository razaRepository)
    {
        private readonly IPaisRepository _paisRepository = paisRepository;
        private readonly IRazaRepository _razaRepository = razaRepository;

        public async Task<List<Pais>> GetAllAsync()
        {
            return await _paisRepository
                .GetAllAsync();
        }

        public async Task<Pais> GetByGuidAsync(Guid pais_guid)
        {
            Pais unPais = await _paisRepository
                .GetByGuidAsync(pais_guid);

            if (unPais.Uuid == Guid.Empty)
                throw new AppValidationException($"Pais no encontrado con el guid {pais_guid}");

            return unPais;
        }

        public async Task<List<Raza>> GetBreedsAsync(Guid pais_guid)
        {
            Pais unPais = await _paisRepository
                .GetByGuidAsync(pais_guid);

            if (unPais.Uuid == Guid.Empty)
                throw new AppValidationException($"Pais no encontrado con el id {pais_guid}");

            var razasAsociadas = await _razaRepository
                .GetByCountryAsync(pais_guid);

            if (razasAsociadas.Count == 0)
                throw new AppValidationException($"Pais {unPais.Nombre} no tiene razas asociadas");

            return razasAsociadas;
        }
    }
}
