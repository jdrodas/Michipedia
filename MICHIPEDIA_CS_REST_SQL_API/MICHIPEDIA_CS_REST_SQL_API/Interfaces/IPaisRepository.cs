using MICHIPEDIA_CS_REST_SQL_API.Models;

namespace MICHIPEDIA_CS_REST_SQL_API.Interfaces
{
    public interface IPaisRepository
    {
        public Task<List<Pais>> GetAllAsync();
        public Task<Pais> GetByGuidAsync(Guid pais_guid);
        public Task<Pais> GetCountryByNameAndContinentAsync(Pais unPais);
        public Task<string> GetContinentByNameAsync(string continente_nombre);
        public Task<bool> CreateAsync(Pais unPais);
        public Task<bool> UpdateAsync(Pais unPais);
    }
}
