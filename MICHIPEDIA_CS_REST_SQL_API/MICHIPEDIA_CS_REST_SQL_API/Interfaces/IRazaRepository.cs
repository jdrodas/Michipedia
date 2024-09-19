using MICHIPEDIA_CS_REST_SQL_API.Models;

namespace MICHIPEDIA_CS_REST_SQL_API.Interfaces
{
    public interface IRazaRepository
    {
        public Task<List<Raza>> GetAllAsync();
        public Task<Raza> GetByGuidAsync(Guid raza_guid);
        public Task<List<Raza>> GetByCountryAsync(Guid pais_guid);
    }
}
