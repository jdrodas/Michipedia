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

        public async Task<RazaCaracterizada> GetByIdAsync(string raza_id)
        {
            Raza unaRaza = await _razaRepository
                .GetByIdAsync(raza_id);

            if (string.IsNullOrEmpty(unaRaza.Id))
                throw new AppValidationException($"Raza no encontrada con el id {raza_id}");

            //Aqui transformamos esa raza en una raza Caracterizada
            RazaCaracterizada unaRazaCaracterizada = new()
            {
                Id = unaRaza.Id,
                Nombre = unaRaza.Nombre,
                Descripcion = unaRaza.Descripcion,
                Pais = unaRaza.Pais,
                Caracteristicas = await _razaRepository
                            .GetCharacteristicsByIdAsync(raza_id)
            };

            return unaRazaCaracterizada;
        }

        //public async Task<Raza> CreateAsync(Raza unaRaza)
        //{
        //    string resultadoValidacionDatos = ValidaDatos(unaRaza);

        //    if (!string.IsNullOrEmpty(resultadoValidacionDatos))
        //        throw new AppValidationException(resultadoValidacionDatos);

        //    var paisExistente = await _paisRepository
        //        .GetCountryByNameAndContinentAsync(unaRaza.Pais!);

        //    if(paisExistente.Uuid == Guid.Empty)
        //        throw new AppValidationException($"No existe registrado el pais de origen de la raza {unaRaza.Pais}");

        //    var razaExistente = await _razaRepository
        //        .GetByNameAsync(unaRaza.Nombre!);

        //    if (razaExistente.Uuid != Guid.Empty)
        //        throw new AppValidationException($"Ya existe la raza {unaRaza.Nombre}");

        //    try
        //    {
        //        bool resultado = await _razaRepository
        //            .CreateAsync(unaRaza);

        //        if (!resultado)
        //            throw new AppValidationException("Operación ejecutada pero no generó cambios");

        //        razaExistente = await _razaRepository
        //            .GetByNameAsync(unaRaza.Nombre!);
        //    }
        //    catch (DbOperationException)
        //    {
        //        throw;
        //    }

        //    return razaExistente;
        //}

        //private static string ValidaDatos(Raza unaRaza)
        //{
        //    if (string.IsNullOrEmpty(unaRaza.Nombre))
        //        return ("El nombre de la raza no puede estar vacío");

        //    if (string.IsNullOrEmpty(unaRaza.Descripcion))
        //        return ("La descripción de la raza no puede estar vacío");

        //    if (string.IsNullOrEmpty(unaRaza.Pais))
        //        return ("El país de origen de la raza no puede estar vacío");

        //    return string.Empty;
        //}
    }
}
