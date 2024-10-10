using MICHIPEDIA_CS_REST_NoSQL_API.Models;

namespace MICHIPEDIA_CS_REST_NoSQL_API.Interfaces
{
    public interface ICaracteristicaRepository
    {
        public Task<List<Caracteristica>> GetAllAsync();
        public Task<Caracteristica> GetByIdAsync(string caracteristica_id);
        
        //public Task<CaracteristicaValorada> GetDetailedCharacteristicByGuidAsync(Guid caracteristica_guid);
    }
}
