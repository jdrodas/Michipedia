using MICHIPEDIA_CS_REST_SQL_API.Models;

namespace MICHIPEDIA_CS_REST_SQL_API.Interfaces
{
    public interface IPaisRepository
    {
        public Task<List<Pais>> GetAllAsync();
        public Task<Pais> GetByGuidAsync(Guid pais_guid);
    }
}
