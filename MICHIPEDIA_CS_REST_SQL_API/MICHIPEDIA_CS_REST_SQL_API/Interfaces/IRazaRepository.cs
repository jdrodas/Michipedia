using MICHIPEDIA_CS_REST_SQL_API.Models;

namespace MICHIPEDIA_CS_REST_SQL_API.Interfaces
{
    public interface IRazaRepository
    {
        public Task<List<Raza>> GetAllAsync();
    }
}
