using MICHIPEDIA_CS_REST_SQL_API.Interfaces;
using MICHIPEDIA_CS_REST_SQL_API.Models;

namespace MICHIPEDIA_CS_REST_SQL_API.Services
{
    public class PaisService(IPaisRepository paisRepository)
    {
        private readonly IPaisRepository _paisRepository = paisRepository;
        
        public async Task<List<Pais>> GetAllAsync()
        {
            return await _paisRepository
                .GetAllAsync();
        }
    }
}
