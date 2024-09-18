using MICHIPEDIA_CS_REST_SQL_API.Interfaces;
using MICHIPEDIA_CS_REST_SQL_API.Models;
using MICHIPEDIA_CS_REST_SQL_API.Repositories;

namespace MICHIPEDIA_CS_REST_SQL_API.Services
{
    public class RazaService(IRazaRepository razaRepository)
    {
        private readonly IRazaRepository _razaRepository = razaRepository;

        public async Task<List<Raza>> GetAllAsync()
        {
            return await _razaRepository
                .GetAllAsync();
        }
    }
}
