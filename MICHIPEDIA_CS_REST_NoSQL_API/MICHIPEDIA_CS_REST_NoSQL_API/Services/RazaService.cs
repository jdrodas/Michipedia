using MICHIPEDIA_CS_REST_NoSQL_API.Exceptions;
using MICHIPEDIA_CS_REST_NoSQL_API.Interfaces;
using MICHIPEDIA_CS_REST_NoSQL_API.Models;
using MICHIPEDIA_CS_REST_NoSQL_API.Repositories;
using System.Reflection.PortableExecutable;

namespace MICHIPEDIA_CS_REST_NoSQL_API.Services
{
    public class RazaService(IRazaRepository razaRepository
                            ,IPaisRepository paisRepository
        )
    {
        private readonly IRazaRepository _razaRepository = razaRepository;
        private readonly IPaisRepository _paisRepository = paisRepository;


        public async Task<List<Raza>> GetAllAsync()
        {
            return await _razaRepository
                .GetAllAsync();
        }

        public async Task<RazaDetallada> GetByIdAsync(string raza_id)
        {
            Raza unaRaza = await _razaRepository
                .GetByIdAsync(raza_id);

            if (string.IsNullOrEmpty(unaRaza.Id))
                throw new AppValidationException($"Raza no encontrada con el id {raza_id}");

            RazaDetallada unaRazaDetallada = new()
            {
                Id = unaRaza.Id,
                Nombre = unaRaza.Nombre,
                Descripcion = unaRaza.Descripcion,
                Pais = unaRaza.Pais,
                Caracteristicas = await _razaRepository
                            .GetCharacteristicsByIdAsync(raza_id),
                Comportamientos = await _razaRepository
                            .GetBehaviorsByIdAsync(raza_id)
            };

            return unaRazaDetallada;
        }
    }
}
