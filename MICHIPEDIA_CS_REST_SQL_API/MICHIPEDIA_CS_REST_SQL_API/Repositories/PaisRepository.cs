using Dapper;
using MICHIPEDIA_CS_REST_SQL_API.DbContexts;
using MICHIPEDIA_CS_REST_SQL_API.Interfaces;
using MICHIPEDIA_CS_REST_SQL_API.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MICHIPEDIA_CS_REST_SQL_API.Repositories
{
    public class PaisRepository(PgsqlDbContext unContexto) : IPaisRepository
    {
        private readonly PgsqlDbContext contextoDB = unContexto;

        public async Task<List<Pais>> GetAllAsync()
        {
            var conexion = contextoDB.CreateConnection();

            string sentenciaSQL = 
                "SELECT pais_uuid uuid, nombre, continente " +
                "FROM core.paises ORDER BY continente, nombre";

            var resultadoDepartamentos = await conexion
                .QueryAsync<Pais>(sentenciaSQL, new DynamicParameters());

            return resultadoDepartamentos.ToList();
        }
    }
}
