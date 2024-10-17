using MICHIPEDIA_CS_REST_NoSQL_API.Exceptions;
using MICHIPEDIA_CS_REST_NoSQL_API.Interfaces;
using MICHIPEDIA_CS_REST_NoSQL_API.Models;
using MICHIPEDIA_CS_REST_NoSQL_API.Repositories;

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

        public async Task<Caracteristica> CreateAsync(Caracteristica unaCaracteristica)
        {
            string resultadoValidacionDatos = ValidaDatos(unaCaracteristica);

            if (!string.IsNullOrEmpty(resultadoValidacionDatos))
                throw new AppValidationException(resultadoValidacionDatos);

            var caracteristicaExistente = await _caracteristicaRepository
                .GetByNameAndDescriptionAsync(unaCaracteristica);

            if (!string.IsNullOrEmpty(caracteristicaExistente.Id))
                throw new AppValidationException($"Ya existe una caracteristica {unaCaracteristica.Nombre} " +
                    $"con descripción \"{unaCaracteristica.Descripcion}\"");

            try
            {
                bool resultado = await _caracteristicaRepository
                    .CreateAsync(unaCaracteristica);

                if (!resultado)
                    throw new AppValidationException("Operación ejecutada pero no generó cambios");

                caracteristicaExistente = await _caracteristicaRepository
                    .GetByNameAndDescriptionAsync(unaCaracteristica);
            }
            catch (DbOperationException)
            {
                throw;
            }

            return caracteristicaExistente;
        }

        private static string ValidaDatos(Caracteristica unaCaracteristica)
        {
            if (string.IsNullOrEmpty(unaCaracteristica.Nombre))
                return ("El nombre de la caracteristica no puede estar vacío");

            if (string.IsNullOrEmpty(unaCaracteristica.Descripcion))
                return ("La descripción de la caracteristica no puede estar vacía");

            return string.Empty;
        }

    }
}
