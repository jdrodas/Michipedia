using Dapper;
using MICHIPEDIA_CS_REST_SQL_API.DbContexts;
using MICHIPEDIA_CS_REST_SQL_API.Interfaces;
using MICHIPEDIA_CS_REST_SQL_API.Models;

namespace MICHIPEDIA_CS_REST_SQL_API.Repositories
{
    public class CaracteristicaRepository(PgsqlDbContext unContexto) : ICaracteristicaRepository
    {
        private readonly PgsqlDbContext contextoDB = unContexto;

        public async Task<List<Caracteristica>> GetAllAsync()
        {
            var conexion = contextoDB.CreateConnection();

            string sentenciaSQL =
                "SELECT DISTINCT caracteristica_uuid uuid, nombre, descripcion " +
                "FROM caracteristicas ORDER BY nombre";

            var resultadoCaracteristicas = await conexion
                .QueryAsync<Caracteristica>(sentenciaSQL, new DynamicParameters());

            return resultadoCaracteristicas.ToList();
        }
    }
}
