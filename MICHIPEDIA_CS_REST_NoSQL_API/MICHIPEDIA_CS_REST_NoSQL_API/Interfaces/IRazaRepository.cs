using MICHIPEDIA_CS_REST_NoSQL_API.Models;

namespace MICHIPEDIA_CS_REST_NoSQL_API.Interfaces
{
    public interface IRazaRepository
    {
        public Task<List<Raza>> GetAllAsync();
        public Task<Raza> GetByIdAsync(string raza_id);
        public Task<List<Raza>> GetByCountryAsync(string descripcion_pais);
        public Task<List<CaracteristicaSimplificada>> GetCharacteristicsByIdAsync(string raza_id);
        public Task<List<ComportamientoSimplificado>> GetBehaviorsByIdAsync(string raza_id);
    }
}
