using MICHIPEDIA_CS_REST_SQL_API.Models;

namespace MICHIPEDIA_CS_REST_SQL_API.Interfaces
{
    public interface ICaracteristicaRepository
    {
        public Task<List<Caracteristica>> GetAllAsync();
    }
}
