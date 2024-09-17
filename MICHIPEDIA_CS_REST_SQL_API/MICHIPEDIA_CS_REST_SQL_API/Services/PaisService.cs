using MICHIPEDIA_CS_REST_SQL_API.Exceptions;
using MICHIPEDIA_CS_REST_SQL_API.Interfaces;
using MICHIPEDIA_CS_REST_SQL_API.Models;

namespace MICHIPEDIA_CS_REST_SQL_API.Services
{
    public class PaisService(IPaisRepository paisRepository)
    {
        private readonly IPaisRepository _paisRepository = paisRepository;
        
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
    }
}
