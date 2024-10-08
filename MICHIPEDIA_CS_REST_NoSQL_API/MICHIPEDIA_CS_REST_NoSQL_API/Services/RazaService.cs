using MICHIPEDIA_CS_REST_SQL_API.Exceptions;
using MICHIPEDIA_CS_REST_SQL_API.Interfaces;
using MICHIPEDIA_CS_REST_SQL_API.Models;
using MICHIPEDIA_CS_REST_SQL_API.Repositories;

namespace MICHIPEDIA_CS_REST_SQL_API.Services
{
    public class RazaService(IRazaRepository razaRepository, 
                            IPaisRepository paisRepository)
    {
        private readonly IRazaRepository _razaRepository = razaRepository;
        private readonly IPaisRepository _paisRepository = paisRepository;


        public async Task<List<Raza>> GetAllAsync()
        {
            return await _razaRepository
                .GetAllAsync();
        }

        public async Task<RazaDetallada> GetByGuidAsync(Guid raza_guid)
        {
            Raza unaRaza = await _razaRepository
                .GetByGuidAsync(raza_guid);

            if (unaRaza.Uuid == Guid.Empty)
                throw new AppValidationException($"Raza no encontrada con el guid {raza_guid}");

            RazaDetallada unaRazaDetallada = await _razaRepository
                .GetDetailedBreedByGuidAsync(raza_guid);

            return unaRazaDetallada;
        }

        public async Task<Raza> CreateAsync(Raza unaRaza)
        {
            string resultadoValidacionDatos = ValidaDatos(unaRaza);

            if (!string.IsNullOrEmpty(resultadoValidacionDatos))
                throw new AppValidationException(resultadoValidacionDatos);

            var paisExistente = await _paisRepository
                .GetCountryByNameAndContinentAsync(unaRaza.Pais!);

            if(paisExistente.Uuid == Guid.Empty)
                throw new AppValidationException($"No existe registrado el pais de origen de la raza {unaRaza.Pais}");

            var razaExistente = await _razaRepository
                .GetByNameAsync(unaRaza.Nombre!);

            if (razaExistente.Uuid != Guid.Empty)
                throw new AppValidationException($"Ya existe la raza {unaRaza.Nombre}");

            try
            {
                bool resultado = await _razaRepository
                    .CreateAsync(unaRaza);

                if (!resultado)
                    throw new AppValidationException("Operación ejecutada pero no generó cambios");

                razaExistente = await _razaRepository
                    .GetByNameAsync(unaRaza.Nombre!);
            }
            catch (DbOperationException)
            {
                throw;
            }

            return razaExistente;
        }

        private static string ValidaDatos(Raza unaRaza)
        {
            if (string.IsNullOrEmpty(unaRaza.Nombre))
                return ("El nombre de la raza no puede estar vacío");

            if (string.IsNullOrEmpty(unaRaza.Descripcion))
                return ("La descripción de la raza no puede estar vacío");

            if (string.IsNullOrEmpty(unaRaza.Pais))
                return ("El país de origen de la raza no puede estar vacío");

            return string.Empty;
        }
    }
}
