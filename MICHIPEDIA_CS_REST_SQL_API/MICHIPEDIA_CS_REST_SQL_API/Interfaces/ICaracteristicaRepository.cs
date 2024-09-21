using MICHIPEDIA_CS_REST_SQL_API.Models;

namespace MICHIPEDIA_CS_REST_SQL_API.Interfaces
{
    public interface ICaracteristicaRepository
    {
        public Task<List<Caracteristica>> GetAllAsync();
        public Task<Caracteristica> GetByGuidAsync(Guid caracteristica_guid);
        public Task<CaracteristicaValorada> GetDetailedCharacteristicByGuidAsync(Guid caracteristica_guid);
    }
}
