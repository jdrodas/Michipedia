using MICHIPEDIA_CS_REST_NoSQL_API.Models;

namespace MICHIPEDIA_CS_REST_NoSQL_API.Interfaces
{
    public interface ICaracteristicaRepository
    {
        public Task<List<Caracteristica>> GetAllAsync();
        public Task<Caracteristica> GetByIdAsync(string caracteristica_id);

        public Task<Caracteristica> GetByNameAsync(string caracteristica_nombre);

        public Task<Caracteristica> GetByNameAndDescriptionAsync(Caracteristica unaCaracteristica);

        //public Task<CaracteristicaValorada> GetDetailedCharacteristicByGuidAsync(Guid caracteristica_guid);

        public Task<bool> CreateAsync(Caracteristica unaCaracteristica);

        public Task<bool> UpdateAsync(Caracteristica unaCaracteristica);

        public Task<bool> RemoveAsync(string caracteristica_id);
    }
}
