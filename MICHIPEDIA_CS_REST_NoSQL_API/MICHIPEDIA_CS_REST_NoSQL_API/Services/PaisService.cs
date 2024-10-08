using MICHIPEDIA_CS_REST_NoSQL_API.Exceptions;
using MICHIPEDIA_CS_REST_NoSQL_API.Interfaces;
using MICHIPEDIA_CS_REST_NoSQL_API.Models;

namespace MICHIPEDIA_CS_REST_NoSQL_API.Services
{
    public class PaisService(IPaisRepository paisRepository
                            //,IRazaRepository razaRepository
        )
    {
        private readonly IPaisRepository _paisRepository = paisRepository;
        //private readonly IRazaRepository _razaRepository = razaRepository;

        public async Task<IEnumerable<Pais>> GetAllAsync()
        {
            return await _paisRepository
                .GetAllAsync();
        }

        public async Task<Pais> GetByIdAsync(string pais_id)
        {
            Pais unPais = await _paisRepository
                .GetByIdAsync(pais_id);

            if (string.IsNullOrEmpty(unPais.Id))
                throw new AppValidationException($"Pais no encontrado con el Id {pais_id}");

            return unPais;
        }

        //public async Task<List<Raza>> GetBreedsAsync(Guid pais_guid)
        //{
        //    Pais unPais = await _paisRepository
        //        .GetByGuidAsync(pais_guid);

        //    if (unPais.Uuid == Guid.Empty)
        //        throw new AppValidationException($"Pais no encontrado con el id {pais_guid}");

        //    var razasAsociadas = await _razaRepository
        //        .GetByCountryAsync(pais_guid);

        //    if (razasAsociadas.Count == 0)
        //        throw new AppValidationException($"Pais {unPais.Nombre} no tiene razas asociadas");

        //    return razasAsociadas;
        //}

        //public async Task<Pais> CreateAsync(Pais unPais)
        //{
        //    string resultadoValidacionDatos = ValidaDatos(unPais);

        //    if (!string.IsNullOrEmpty(resultadoValidacionDatos))
        //        throw new AppValidationException(resultadoValidacionDatos);

        //    var continenteExistente = await _paisRepository
        //        .GetContinentByNameAsync(unPais.Continente!);

        //    if (string.IsNullOrEmpty(continenteExistente))
        //        throw new AppValidationException($"'No existe un continente {unPais.Continente} registrado previamente");

        //    var paisExistente = await _paisRepository
        //        .GetCountryByNameAndContinentAsync(unPais);

        //    if (paisExistente.Uuid != Guid.Empty)
        //        throw new AppValidationException($"Ya existe el pais {unPais.Nombre} " +
        //            $"ubicado en el continente {unPais.Continente}");

        //    try
        //    {
        //        bool resultado = await _paisRepository
        //            .CreateAsync(unPais);

        //        if (!resultado)
        //            throw new AppValidationException("Operación ejecutada pero no generó cambios");

        //        paisExistente = await _paisRepository
        //            .GetCountryByNameAndContinentAsync(unPais);
        //    }
        //    catch (DbOperationException)
        //    {
        //        throw;
        //    }

        //    return paisExistente;
        //}

        //public async Task<Pais> UpdateAsync(Pais unPais)
        //{
        //    string resultadoValidacionDatos = ValidaDatos(unPais);

        //    if (!string.IsNullOrEmpty(resultadoValidacionDatos))
        //        throw new AppValidationException(resultadoValidacionDatos);

        //    var continenteExistente = await _paisRepository
        //        .GetContinentByNameAsync(unPais.Continente!);

        //    if (string.IsNullOrEmpty(continenteExistente))
        //        throw new AppValidationException($"'No existe un continente {unPais.Continente} registrado previamente");

        //    var paisExistente = await _paisRepository
        //        .GetCountryByNameAndContinentAsync(unPais);

        //    if (paisExistente.Uuid != Guid.Empty && paisExistente.Uuid != unPais.Uuid)
        //        throw new AppValidationException($"Ya existe el pais {unPais.Nombre} " +
        //            $"ubicado en el continente {unPais.Continente}");

        //    try
        //    {
        //        bool resultadoAccion = await _paisRepository
        //            .UpdateAsync(unPais);

        //        if (!resultadoAccion)
        //            throw new AppValidationException("Operación ejecutada pero no generó cambios en la DB");

        //        paisExistente = await _paisRepository
        //            .GetByGuidAsync(unPais.Uuid);
        //    }
        //    catch (DbOperationException)
        //    {
        //        throw;
        //    }

        //    return paisExistente;
        //}

        //public async Task<Pais> RemoveAsync(Guid pais_guid)
        //{
        //    var paisExistente = await _paisRepository
        //        .GetByGuidAsync(pais_guid);

        //    if (paisExistente.Uuid == Guid.Empty)
        //        throw new AppValidationException($"No existe un pais identificado con el Guid {pais_guid} registrado previamente");

        //    int totalRazasAsociadas = await _paisRepository
        //        .GetTotalAssociatedBreedsByCountryGuidAsync(pais_guid);

        //    if (totalRazasAsociadas != 0)
        //        throw new AppValidationException($"Pais {paisExistente.Nombre} tiene asociado {totalRazasAsociadas} razas. No se puede eliminar.");

        //    try
        //    {
        //        bool resultadoAccion = await _paisRepository
        //            .RemoveAsync(pais_guid);

        //        if (!resultadoAccion)
        //            throw new AppValidationException("Operación ejecutada pero no generó cambios en la DB");
        //    }
        //    catch (DbOperationException)
        //    {
        //        throw;
        //    }

        //    return paisExistente;

        //}

        //private static string ValidaDatos(Pais unPais)
        //{
        //    if (string.IsNullOrEmpty(unPais.Nombre))
        //        return ("El país de origen de la raza no puede estar vacío");

        //    if (string.IsNullOrEmpty(unPais.Continente))
        //        return ("El continente de origen de la raza no puede estar vacío");

        //    return string.Empty;
        //}
    }
}
