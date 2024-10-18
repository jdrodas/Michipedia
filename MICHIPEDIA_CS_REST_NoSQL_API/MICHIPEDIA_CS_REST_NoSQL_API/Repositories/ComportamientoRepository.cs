using MICHIPEDIA_CS_REST_NoSQL_API.DbContexts;
using MICHIPEDIA_CS_REST_NoSQL_API.Interfaces;
using MICHIPEDIA_CS_REST_NoSQL_API.Models;
using MongoDB.Driver;

namespace MICHIPEDIA_CS_REST_NoSQL_API.Repositories
{
    public class ComportamientoRepository(MongoDbContext unContexto) : IComportamientoRepository
    {
        private readonly MongoDbContext contextoDB = unContexto;

        public async Task<List<Comportamiento>> GetAllAsync()
        {
            var conexion = contextoDB.CreateConnection();
            var coleccionComportamientos = conexion
                .GetCollection<Comportamiento>(contextoDB.ConfiguracionColecciones.ColeccionComportamientos);

            var losComportamientos = await coleccionComportamientos
                .Find(_ => true)
                .SortBy(comportamiento => comportamiento.Nombre)
                .ToListAsync();

            return losComportamientos;
        }

        public async Task<Comportamiento> GetByIdAsync(string comportamiento_id)
        {
            Comportamiento unComportamiento = new();

            var conexion = contextoDB.CreateConnection();
            var coleccionComportamientos = conexion
                .GetCollection<Comportamiento>(contextoDB.ConfiguracionColecciones.ColeccionComportamientos);

            var resultado = await coleccionComportamientos
                .Find(comportamiento => comportamiento.Id == comportamiento_id)
                .FirstOrDefaultAsync();

            if (resultado is not null)
                unComportamiento = resultado;

            return unComportamiento;
        }

        public async Task<Comportamiento> GetByNameAsync(string comportamiento_nombre)
        {
            Comportamiento unComportamiento = new();

            var conexion = contextoDB.CreateConnection();
            var coleccionComportamientos = conexion
                .GetCollection<Comportamiento>(contextoDB.ConfiguracionColecciones.ColeccionComportamientos);

            var resultado = await coleccionComportamientos
                .Find(comportamiento => comportamiento.Nombre == comportamiento_nombre)
                .FirstOrDefaultAsync();

            if (resultado is not null)
                unComportamiento = resultado;

            return unComportamiento;
        }

        public async Task<Comportamiento> GetByNameAndDescriptionAsync(Comportamiento unComportamiento)
        {
            Comportamiento comportamientoEncontrado = new();

            var conexion = contextoDB.CreateConnection();
            var coleccionComportamientos = conexion
                .GetCollection<Comportamiento>(contextoDB.ConfiguracionColecciones.ColeccionComportamientos);

            var builder = Builders<Comportamiento>.Filter;
            var filtro = builder.And(
                builder.Eq(comportamiento => comportamiento.Nombre, unComportamiento.Nombre),
                builder.Eq(comportamiento => comportamiento.Descripcion, unComportamiento.Descripcion));

            var resultado = await coleccionComportamientos
                .Find(filtro)
                .FirstOrDefaultAsync();

            if (resultado is not null)
                comportamientoEncontrado = resultado;

            return comportamientoEncontrado;
        }

        public async Task<bool> CreateAsync(Comportamiento unComportamiento)
        {
            bool resultadoAccion = false;

            var conexion = contextoDB.CreateConnection();
            var coleccionComportamientos = conexion
                .GetCollection<Comportamiento>(contextoDB.ConfiguracionColecciones.ColeccionComportamientos);


            await coleccionComportamientos
                .InsertOneAsync(unComportamiento);

            var resultado = await GetByNameAndDescriptionAsync(unComportamiento);

            if (resultado is not null)
                resultadoAccion = true;

            return resultadoAccion;
        }

        public async Task<bool> UpdateAsync(Comportamiento unComportamiento)
        {
            bool resultadoAccion = false;

            var conexion = contextoDB.CreateConnection();
            var coleccionComportamientos = conexion
                .GetCollection<Comportamiento>(contextoDB.ConfiguracionColecciones.ColeccionComportamientos);

            var resultado = await coleccionComportamientos
                .ReplaceOneAsync(comportamiento => comportamiento.Id == unComportamiento.Id, unComportamiento);

            if (resultado.IsAcknowledged)
                resultadoAccion = true;

            return resultadoAccion;
        }

        public async Task<bool> RemoveAsync(string comportamiento_id)
        {
            bool resultadoAccion = false;

            var conexion = contextoDB.CreateConnection();
            var coleccionComportamientos = conexion
                .GetCollection<Comportamiento>(contextoDB.ConfiguracionColecciones.ColeccionComportamientos);

            var resultado = await coleccionComportamientos
                .DeleteOneAsync(comportamiento => comportamiento.Id == comportamiento_id);

            if (resultado.IsAcknowledged)
                resultadoAccion = true;

            return resultadoAccion;
        }
    }
}
