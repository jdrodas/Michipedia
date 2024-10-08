using Dapper;
using MICHIPEDIA_CS_REST_SQL_API.DbContexts;
using MICHIPEDIA_CS_REST_SQL_API.Interfaces;
using MICHIPEDIA_CS_REST_SQL_API.Models;

namespace MICHIPEDIA_CS_REST_SQL_API.Repositories
{
    public class ResumenRepository(PgsqlDbContext unContexto) : IResumenRepository
    {
        private readonly PgsqlDbContext contextoDB = unContexto;

        public async Task<Resumen> GetAllAsync()
        {
            Resumen unResumen = new();

            var conexion = contextoDB.CreateConnection();

            string sentenciaSQL = "SELECT COUNT(id) total FROM core.razas";
            unResumen.Razas = await conexion
                .QueryFirstAsync<int>(sentenciaSQL, new DynamicParameters());

            sentenciaSQL = "SELECT COUNT(id) total FROM core.paises";
            unResumen.Paises = await conexion
                .QueryFirstAsync<int>(sentenciaSQL, new DynamicParameters());

            sentenciaSQL = "SELECT COUNT(id) total FROM core.caracteristicas";
            unResumen.Caracteristicas = await conexion
                .QueryFirstAsync<int>(sentenciaSQL, new DynamicParameters());

            sentenciaSQL = "SELECT COUNT(id) total FROM core.comportamientos";
            unResumen.Comportamientos = await conexion
                .QueryFirstAsync<int>(sentenciaSQL, new DynamicParameters());

            return unResumen;
        }
    }
}