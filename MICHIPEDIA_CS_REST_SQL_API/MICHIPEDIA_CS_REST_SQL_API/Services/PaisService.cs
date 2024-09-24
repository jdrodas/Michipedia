using MICHIPEDIA_CS_REST_SQL_API.Exceptions;
using MICHIPEDIA_CS_REST_SQL_API.Interfaces;
using MICHIPEDIA_CS_REST_SQL_API.Models;

namespace MICHIPEDIA_CS_REST_SQL_API.Services
{
    public class PaisService(IPaisRepository paisRepository,
                             IRazaRepository razaRepository)
    {
        private readonly IPaisRepository _paisRepository = paisRepository;
        private readonly IRazaRepository _razaRepository = razaRepository;

        public async Task<List<Pais>> GetAllAsync()
        {
            return await _paisRepository
                .GetAllAsync();
        }

        public async Task<Pais> GetByGuidAsync(Guid pais_guid)
        {
            Pais unPais = await _paisRepository
                .GetByGuidAsync(pais_guid);

            if (unPais.Uuid == Guid.Empty)
                throw new AppValidationException($"Pais no encontrado con el guid {pais_guid}");

            return unPais;
        }

        public async Task<List<Raza>> GetBreedsAsync(Guid pais_guid)
        {
            Pais unPais = await _paisRepository
                .GetByGuidAsync(pais_guid);

            if (unPais.Uuid == Guid.Empty)
                throw new AppValidationException($"Pais no encontrado con el id {pais_guid}");

            var razasAsociadas = await _razaRepository
                .GetByCountryAsync(pais_guid);

            if (razasAsociadas.Count == 0)
                throw new AppValidationException($"Pais {unPais.Nombre} no tiene razas asociadas");

            return razasAsociadas;
        }

        public async Task<Pais> CreateAsync(Pais unPais)
        {
            string resultadoValidacionDatos = ValidaDatos(unPais);

            if (!string.IsNullOrEmpty(resultadoValidacionDatos))
                throw new AppValidationException(resultadoValidacionDatos);

            var paisExistente = await _paisRepository
                .GetCountryByNameAndContinentAsync(unPais);

            if (paisExistente.Uuid != Guid.Empty)
                throw new AppValidationException($"Ya existe el pais {unPais.Nombre} " +
                    $"ubicado en el continente {unPais.Continente}");

            //TODO: Crear una validación sobre la existencia del continente
            //string continenteExistente = await _paisRepository
            //    .GetContinentByNameAsync(unPais.Continente!);

            //if (continenteExistente == string.Empty)
            //    throw new AppValidationException($"El continente {unPais.Continente} no se encuentra registrado");

            try
            {
                bool resultado = await _paisRepository
                    .CreateAsync(unPais);

                if (!resultado)
                    throw new AppValidationException("Operación ejecutada pero no generó cambios");

                paisExistente = await _paisRepository
                    .GetCountryByNameAndContinentAsync(unPais);
            }
            catch (DbOperationException)
            {
                throw;
            }

            return paisExistente;
        }

        private static string ValidaDatos(Pais unPais)
        {
            if (string.IsNullOrEmpty(unPais.Nombre))
                return ("El país de origen de la raza no puede estar vacío");

            if (string.IsNullOrEmpty(unPais.Continente))
                return ("El continente de origen de la raza no puede estar vacío");

            return string.Empty;
        }
    }
}
