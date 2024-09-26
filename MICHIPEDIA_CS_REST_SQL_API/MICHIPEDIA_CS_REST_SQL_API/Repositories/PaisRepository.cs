using Dapper;
using MICHIPEDIA_CS_REST_SQL_API.DbContexts;
using MICHIPEDIA_CS_REST_SQL_API.Exceptions;
using MICHIPEDIA_CS_REST_SQL_API.Interfaces;
using MICHIPEDIA_CS_REST_SQL_API.Models;
using Npgsql;
using System.Data;

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

        public async Task<Pais> GetCountryByNameAndContinentAsync(Pais unPais)
        {
            Pais paisExistente = new();

            var conexion = contextoDB.CreateConnection();

            DynamicParameters parametrosSentencia = new();
            parametrosSentencia.Add("@pais_nombre", unPais.Nombre,
                                    DbType.String, ParameterDirection.Input);
            parametrosSentencia.Add("@pais_continente", unPais.Continente,
                                    DbType.String, ParameterDirection.Input);

            string sentenciaSQL =
                "SELECT pais_uuid uuid, nombre, continente " +
                "FROM core.paises " +
                "WHERE LOWER(nombre) = LOWER(@pais_nombre) " +
                "AND LOWER(continente) = LOWER(@pais_continente)";

            var resultado = await conexion.QueryAsync<Pais>(sentenciaSQL,
                parametrosSentencia);

            if (resultado.Any())
                paisExistente = resultado.First();

            return paisExistente;
        }

        public async Task<Pais> GetCountryByNameAndContinentAsync(string pais_continente)
        {
            string[] datosPais = pais_continente.Split('-');

            Pais paisBuscado = new()
            {
                Nombre = datosPais[0].Trim(),
                Continente = datosPais[1].Trim()
            };

            var paisExistente = await GetCountryByNameAndContinentAsync(paisBuscado);

            return paisExistente;
        }

        public async Task<string> GetContinentByNameAsync(string continente_nombre)
        {
            string nombreContinente = string.Empty;

            var conexion = contextoDB.CreateConnection();

            DynamicParameters parametrosSentencia = new();
            parametrosSentencia.Add("@continente_nombre", continente_nombre,
                                    DbType.String, ParameterDirection.Input);

            string sentenciaSQL =
                "SELECT distinct continente " +
                "FROM core.v_info_continentes " +
                "WHERE LOWER(continente) = LOWER(@continente_nombre)";

            var resultado = await conexion.QueryAsync<string>(sentenciaSQL,
                parametrosSentencia);

            if (resultado.Any())
                nombreContinente = resultado.First();

            return nombreContinente;
        }

        public async Task<int> GetTotalAssociatedBreedsByCountryGuidAsync(Guid pais_guid)
        {
            var conexion = contextoDB.CreateConnection();

            DynamicParameters parametrosSentencia = new();
            parametrosSentencia.Add("@pais_guid", pais_guid,
                                    DbType.Guid, ParameterDirection.Input);

            //Aqui colocamos la informacion nutricional
            string sentenciaSQL = "SELECT count(*) totalRegistros " +
                "FROM core.v_info_razas " +
                "WHERE pais_uuid = @pais_guid";

            var totalRegistros = await conexion
                .QueryAsync<int>(sentenciaSQL, parametrosSentencia);

            return totalRegistros.FirstOrDefault();
        }


        public async Task<bool> CreateAsync(Pais unPais)
        {
            bool resultadoAccion = false;

            try
            {
                var conexion = contextoDB.CreateConnection();

                string procedimiento = "core.p_insertar_pais";

                var parametros = new
                {
                    p_nombre = unPais.Nombre,
                    p_continente = unPais.Continente
                };

                var cantidadFilas = await conexion
                    .ExecuteAsync(
                        procedimiento,
                        parametros,
                        commandType: CommandType.StoredProcedure);

                if (cantidadFilas != 0)
                    resultadoAccion = true;
            }
            catch (NpgsqlException error)
            {
                throw new DbOperationException(error.Message);
            }

            return resultadoAccion;
        }

        public async Task<bool> UpdateAsync(Pais unPais)
        {
            bool resultadoAccion = false;

            var paisExistente = await GetByGuidAsync(unPais.Uuid);

            if (paisExistente.Uuid == Guid.Empty)
                throw new DbOperationException($"No se puede actualizar. No existe la fruta {unPais.Nombre!}.");

            try
            {
                var conexion = contextoDB.CreateConnection();

                string procedimiento = "core.p_actualizar_pais";
                var parametros = new
                {
                    p_uuid = unPais.Uuid,
                    p_nombre = unPais.Nombre,
                    p_continente = unPais.Continente
                };

                var cantidad_filas = await conexion.ExecuteAsync(
                    procedimiento,
                    parametros,
                    commandType: CommandType.StoredProcedure);

                if (cantidad_filas != 0)
                    resultadoAccion = true;
            }
            catch (NpgsqlException error)
            {
                throw new DbOperationException(error.Message);
            }

            return resultadoAccion;
        }

        public async Task<bool> RemoveAsync(Guid pais_guid)
        {
            bool resultadoAccion = false;

            try
            {

                var conexion = contextoDB.CreateConnection();

                string procedimiento = "core.p_eliminar_pais";
                var parametros = new
                {
                    p_uuid = pais_guid
                };

                var cantidad_filas = await conexion.ExecuteAsync(
                    procedimiento,
                    parametros,
                    commandType: CommandType.StoredProcedure);

                if (cantidad_filas != 0)
                    resultadoAccion = true;
            }
            catch (NpgsqlException error)
            {
                throw new DbOperationException(error.Message);
            }

            return resultadoAccion;
        }
    }
}
