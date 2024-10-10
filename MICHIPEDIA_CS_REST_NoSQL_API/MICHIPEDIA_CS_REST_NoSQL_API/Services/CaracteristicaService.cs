using MICHIPEDIA_CS_REST_NoSQL_API.Exceptions;
using MICHIPEDIA_CS_REST_NoSQL_API.Interfaces;
using MICHIPEDIA_CS_REST_NoSQL_API.Models;

namespace MICHIPEDIA_CS_REST_NoSQL_API.Services
{
    public class CaracteristicaService(ICaracteristicaRepository caracteristicaRepository)
    {
        private readonly ICaracteristicaRepository _caracteristicaRepository = caracteristicaRepository;

        public async Task<List<Caracteristica>> GetAllAsync()
        {
            return await _caracteristicaRepository
                .GetAllAsync();
        }

        public async Task<Caracteristica> GetByIdAsync(string caracteristica_id)
        {
            Caracteristica unaCaracteristica = await _caracteristicaRepository
                .GetByIdAsync(caracteristica_id);

            if (string.IsNullOrEmpty(unaCaracteristica.Id))
                throw new AppValidationException($"Caracteristica no encontrada con el id {caracteristica_id}");


            return unaCaracteristica;
        }
    }
}
