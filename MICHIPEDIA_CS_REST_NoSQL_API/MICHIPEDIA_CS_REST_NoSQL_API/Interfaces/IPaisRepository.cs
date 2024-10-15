using MICHIPEDIA_CS_REST_NoSQL_API.Models;

namespace MICHIPEDIA_CS_REST_NoSQL_API.Interfaces
{
    public interface IPaisRepository
    {
        public Task<List<Pais>> GetAllAsync();
        public Task<Pais> GetByIdAsync(string pais_id);
        public Task<Pais> GetCountryByNameAndContinentAsync(Pais unPais);
        //public Task<Pais> GetCountryByNameAndContinentAsync(string pais_continente);
        public Task<string> GetContinentByNameAsync(string continente_nombre);
        public Task<long> GetTotalAssociatedBreedsByCountryIdAsync(string pais_id);
        public Task<bool> CreateAsync(Pais unPais);
        public Task<bool> UpdateAsync(Pais unPais);
        public Task<bool> RemoveAsync(string pais_id);
    }
}
