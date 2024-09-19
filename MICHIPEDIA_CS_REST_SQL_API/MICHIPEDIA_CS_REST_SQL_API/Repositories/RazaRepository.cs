using Dapper;
using MICHIPEDIA_CS_REST_SQL_API.DbContexts;
using MICHIPEDIA_CS_REST_SQL_API.Interfaces;
using MICHIPEDIA_CS_REST_SQL_API.Models;
using System.Data;

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

        public async Task<Raza> GetByGuidAsync(Guid raza_guid)
        {
            Raza unaRaza= new();

            var conexion = contextoDB.CreateConnection();

            DynamicParameters parametrosSentencia = new();
            parametrosSentencia.Add("@raza_guid", raza_guid,
                                    DbType.Guid, ParameterDirection.Input);

            string sentenciaSQL =
                "SELECT DISTINCT raza_uuid uuid, nombre, descripcion, pais " +
                "FROM v_info_razas " +
                "WHERE raza_uuid = @raza_guid ";


            var resultado = await conexion.QueryAsync<Raza>(sentenciaSQL,
                parametrosSentencia);

            if (resultado.Any())
                unaRaza = resultado.First();

            return unaRaza;
        }

        public async Task<List<Raza>> GetByCountryAsync(Guid pais_guid)
        {
            List<Raza> razasAsociadas = [];

            var conexion = contextoDB.CreateConnection();

            DynamicParameters parametrosSentencia = new();
            parametrosSentencia.Add("@pais_guid", pais_guid,
                                    DbType.Guid, ParameterDirection.Input);

            string sentenciaSQL =
                "SELECT DISTINCT raza_uuid uuid, nombre, descripcion, pais " +
                "FROM v_info_razas " +
                "WHERE pais_uuid = @pais_guid " +
                "ORDER BY nombre";

            var resultadoRazas = await conexion
                .QueryAsync<Raza>(sentenciaSQL, parametrosSentencia);

            if (resultadoRazas.Any())
                razasAsociadas = resultadoRazas.ToList();

            return razasAsociadas;
        }

        public async Task<RazaCaracterizada> GetCharacterizedBreedByGuidAsync(Guid raza_guid)
        {
            Raza unaRaza = await GetByGuidAsync(raza_guid);

            RazaCaracterizada unaRazaCaracterizada = new()
            {
                Uuid = unaRaza.Uuid,
                Nombre = unaRaza.Nombre,
                Descripcion = unaRaza.Descripcion,
                Pais = unaRaza.Pais,
                Caracteristicas = await GetCharacteristicsDetailsAsync(raza_guid)
            };

            return unaRazaCaracterizada;
        }

        public async Task<List<Caracteristica>> GetCharacteristicsDetailsAsync(Guid raza_guid)
        {
            List<Caracteristica> infoCaracteristicas = [];

            var conexion = contextoDB.CreateConnection();

            DynamicParameters parametrosSentencia = new();
            parametrosSentencia.Add("@raza_guid", raza_guid,
                                    DbType.Guid, ParameterDirection.Input);

            string sentenciaSQL = 
                "SELECT DISTINCT caracteristica_nombre nombre, caracteristica_descripcion descripcion, " +
                "caracteristica_valoracion valoracion " +
                "FROM v_info_caracteristicas_razas " +
                "WHERE raza_uuid = @raza_guid " +
                "ORDER BY caracteristica_nombre ";

            var resultado = await conexion
                .QueryAsync<Caracteristica>(sentenciaSQL, parametrosSentencia);

            if (resultado.Any())
                infoCaracteristicas = resultado.ToList();

            return infoCaracteristicas;
        }
    }
}
