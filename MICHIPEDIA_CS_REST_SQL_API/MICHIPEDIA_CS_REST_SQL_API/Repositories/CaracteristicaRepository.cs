using Dapper;
using MICHIPEDIA_CS_REST_SQL_API.DbContexts;
using MICHIPEDIA_CS_REST_SQL_API.Interfaces;
using MICHIPEDIA_CS_REST_SQL_API.Models;
using System.Data;

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

        public async Task<Caracteristica> GetByGuidAsync(Guid caracteristica_guid)
        {
            Caracteristica unaCaracteristica = new();

            var conexion = contextoDB.CreateConnection();

            DynamicParameters parametrosSentencia = new();
            parametrosSentencia.Add("@caracteristica_guid", caracteristica_guid,
                                    DbType.Guid, ParameterDirection.Input);

            string sentenciaSQL =
                "SELECT caracteristica_uuid uuid, nombre, descripcion " +
                "FROM core.caracteristicas " +
                "WHERE caracteristica_uuid = @caracteristica_guid ";


            var resultado = await conexion.QueryAsync<Caracteristica>(sentenciaSQL,
                parametrosSentencia);

            if (resultado.Any())
                unaCaracteristica = resultado.First();

            return unaCaracteristica;
        }

        public async Task<CaracteristicaValorada> GetDetailedCharacteristicByGuidAsync(Guid caracteristica_guid)
        {
            Caracteristica unaCaracteristica = await GetByGuidAsync(caracteristica_guid);

            CaracteristicaValorada unaCaracteristicaValorada = new()
            {
                Uuid = unaCaracteristica.Uuid,
                Nombre = unaCaracteristica.Nombre,
                Descripcion = unaCaracteristica.Descripcion,
                Valoracion_Caracteristicas = await GetValuedCharacteristicByGuidAsync(caracteristica_guid)
            };

            return unaCaracteristicaValorada;
        }

        private async Task<List<CaracteristicaRaza>> GetValuedCharacteristicByGuidAsync(Guid caracteristica_guid)
        {
            List<CaracteristicaRaza> infoCaracteristicasValoradas = [];

            var conexion = contextoDB.CreateConnection();

            DynamicParameters parametrosSentencia = new();
            parametrosSentencia.Add("@caracteristica_uuid", caracteristica_guid,
                                    DbType.Guid, ParameterDirection.Input);

            string sentenciaSQL =
                "SELECT DISTINCT raza_uuid, raza_nombre, " +
                "caracteristica_valoracion valoracion " +
                "FROM v_info_caracteristicas_razas " +
                "WHERE caracteristica_uuid = @caracteristica_uuid " +
                "ORDER BY raza_nombre";

            var resultado = await conexion
                .QueryAsync<CaracteristicaRaza>(sentenciaSQL, parametrosSentencia);

            if (resultado.Any())
                infoCaracteristicasValoradas = resultado.ToList();

            return infoCaracteristicasValoradas;
        }
    }
}
