using Dapper;
using MICHIPEDIA_CS_REST_SQL_API.DbContexts;
using MICHIPEDIA_CS_REST_SQL_API.Interfaces;
using MICHIPEDIA_CS_REST_SQL_API.Models;

namespace MICHIPEDIA_CS_REST_SQL_API.Repositories
{
    public class RazaRepository(PgsqlDbContext unContexto) : IRazaRepository
    {
        private readonly PgsqlDbContext contextoDB = unContexto;

        public async Task<List<Raza>> GetAllAsync()
        {
            var conexion = contextoDB.CreateConnection();

            string sentenciaSQL =
                "SELECT DISTINCT raza_uuid uuid, nombre, descripcion, pais " +
                "FROM v_info_razas ORDER BY nombre";

            var resultadoRazas = await conexion
                .QueryAsync<Raza>(sentenciaSQL, new DynamicParameters());

            return resultadoRazas.ToList();
        }
    }
}
