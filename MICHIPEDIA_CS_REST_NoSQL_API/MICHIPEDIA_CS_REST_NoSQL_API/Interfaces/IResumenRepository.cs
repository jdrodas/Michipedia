using MICHIPEDIA_CS_REST_NoSQL_API.Models;

namespace MICHIPEDIA_CS_REST_NoSQL_API.Interfaces
{
    public interface IResumenRepository
    {
        public Task<Resumen> GetAllAsync();
    }
}