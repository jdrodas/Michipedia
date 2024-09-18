using Dapper;
using MICHIPEDIA_CS_REST_SQL_API.DbContexts;
using MICHIPEDIA_CS_REST_SQL_API.Interfaces;
using MICHIPEDIA_CS_REST_SQL_API.Models;
using System.Data;
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

            var resultadoPaises = await conexion
                .QueryAsync<Pais>(sentenciaSQL, new DynamicParameters());

            return resultadoPaises.ToList();
        }

        public async Task<Pais> GetByGuidAsync(Guid pais_guid)
        {
            Pais unPais = new();

            var conexion = contextoDB.CreateConnection();

            DynamicParameters parametrosSentencia = new();
            parametrosSentencia.Add("@pais_guid", pais_guid,
                                    DbType.Guid, ParameterDirection.Input);

            string sentenciaSQL =
                "SELECT pais_uuid uuid, nombre, continente " +
                "FROM core.paises " +
                "WHERE pais_uuid = @pais_guid ";


            var resultado = await conexion.QueryAsync<Pais>(sentenciaSQL,
                parametrosSentencia);

            if (resultado.Any())
                unPais = resultado.First();

            return unPais;
        }
    }
}
