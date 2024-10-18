using MICHIPEDIA_CS_REST_NoSQL_API.Exceptions;
using MICHIPEDIA_CS_REST_NoSQL_API.Interfaces;
using MICHIPEDIA_CS_REST_NoSQL_API.Models;

namespace MICHIPEDIA_CS_REST_NoSQL_API.Services
{
    public class ComportamientoService(IComportamientoRepository comportamientoRepository)
    {
        private readonly IComportamientoRepository _comportamientoRepository = comportamientoRepository;

        public async Task<List<Comportamiento>> GetAllAsync()
        {
            return await _comportamientoRepository
                .GetAllAsync();
        }

        public async Task<Comportamiento> GetByIdAsync(string comportamiento_id)
        {
            Comportamiento unComportamiento = await _comportamientoRepository
                .GetByIdAsync(comportamiento_id);

            if (string.IsNullOrEmpty(unComportamiento.Id))
                throw new AppValidationException($"Comportamiento no encontrado con el id {comportamiento_id}");


            return unComportamiento;
        }

        public async Task<Comportamiento> CreateAsync(Comportamiento unComportamiento)
        {
            string resultadoValidacionDatos = ValidaDatos(unComportamiento);

            if (!string.IsNullOrEmpty(resultadoValidacionDatos))
                throw new AppValidationException(resultadoValidacionDatos);

            var comportamientoExistente = await _comportamientoRepository
                .GetByNameAndDescriptionAsync(unComportamiento);

            if (!string.IsNullOrEmpty(comportamientoExistente.Id))
                throw new AppValidationException($"Ya existe un comportamiento {unComportamiento.Nombre} " +
                    $"con descripción \"{unComportamiento.Descripcion}\"");

            try
            {
                bool resultado = await _comportamientoRepository
                    .CreateAsync(unComportamiento);

                if (!resultado)
                    throw new AppValidationException("Operación ejecutada pero no generó cambios");

                comportamientoExistente = await _comportamientoRepository
                    .GetByNameAndDescriptionAsync(unComportamiento);
            }
            catch (DbOperationException)
            {
                throw;
            }

            return comportamientoExistente;
        }

        public async Task<Comportamiento> UpdateAsync(Comportamiento unComportamiento)
        {
            string resultadoValidacionDatos = ValidaDatos(unComportamiento);

            if (!string.IsNullOrEmpty(resultadoValidacionDatos))
                throw new AppValidationException(resultadoValidacionDatos);

            var comportamientoExistente = await _comportamientoRepository
                .GetByIdAsync(unComportamiento.Id!);

            if (string.IsNullOrEmpty(comportamientoExistente.Id))
                throw new AppValidationException($"No existe un comportamiento con el Id {unComportamiento.Id} ");

            comportamientoExistente = await _comportamientoRepository
                .GetByNameAsync(unComportamiento.Nombre!);

            if (string.IsNullOrEmpty(comportamientoExistente.Id))
                throw new AppValidationException($"El comportamiento con el nombre {unComportamiento.Nombre} " +
                    $"no está previamente registrado ");

            if (comportamientoExistente.Id != unComportamiento.Id)
                throw new AppValidationException($"Los Ids para el comportamiento {unComportamiento.Nombre} " +
                    $"no coinciden.");

            try
            {
                bool resultadoAccion = await _comportamientoRepository
                    .UpdateAsync(unComportamiento);

                if (!resultadoAccion)
                    throw new AppValidationException("Operación ejecutada pero no generó cambios en la DB");

                comportamientoExistente = await _comportamientoRepository
                    .GetByIdAsync(unComportamiento.Id!);
            }
            catch (DbOperationException)
            {
                throw;
            }

            return comportamientoExistente;
        }

        public async Task<Comportamiento> RemoveAsync(string comportamiento_id)
        {
            var comportamientoExistente = await _comportamientoRepository
                .GetByIdAsync(comportamiento_id);

            if (string.IsNullOrEmpty(comportamientoExistente.Id))
                throw new AppValidationException($"No existe un comportamiento identificado con el Id {comportamiento_id} registrada previamente");

            //TODO: Contar las razas asociadas a un comportamiento
            /*            
            long totalRazasAsociadas = await _paisRepository
                .GetTotalAssociatedBreedsByCountryIdAsync(pais_id);

            if (totalRazasAsociadas != 0)
                throw new AppValidationException($"Pais {paisExistente.Nombre} tiene asociado {totalRazasAsociadas} razas. No se puede eliminar.");
            */
            try
            {
                bool resultadoAccion = await _comportamientoRepository
                    .RemoveAsync(comportamiento_id);

                if (!resultadoAccion)
                    throw new AppValidationException("Operación ejecutada pero no generó cambios en la DB");
            }
            catch (DbOperationException)
            {
                throw;
            }

            return comportamientoExistente;

        }
        private static string ValidaDatos(Comportamiento unComportamiento)
        {
            if (string.IsNullOrEmpty(unComportamiento.Nombre))
                return ("El nombre del comportamiento no puede estar vacío");

            if (string.IsNullOrEmpty(unComportamiento.Descripcion))
                return ("La descripción del comportamiento no puede estar vacío");

            return string.Empty;
        }
    }
}
