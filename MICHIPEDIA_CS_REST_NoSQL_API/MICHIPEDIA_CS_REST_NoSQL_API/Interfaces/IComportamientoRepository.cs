using MICHIPEDIA_CS_REST_NoSQL_API.Models;

namespace MICHIPEDIA_CS_REST_NoSQL_API.Interfaces
{
    public interface IComportamientoRepository
    {
        public Task<List<Comportamiento>> GetAllAsync();
        public Task<Comportamiento> GetByIdAsync(string comportamiento_id);
        public Task<Comportamiento> GetByNameAsync(string comportamiento_nombre);
        public Task<Comportamiento> GetByNameAndDescriptionAsync(Comportamiento unComportamiento);
        public Task<bool> CreateAsync(Comportamiento unComportamiento);
        public Task<bool> UpdateAsync(Comportamiento unComportamiento);
        public Task<bool> RemoveAsync(string comportamiento_id);
    }
}
