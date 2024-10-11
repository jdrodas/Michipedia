using MICHIPEDIA_CS_REST_NoSQL_API.DbContexts;
using MICHIPEDIA_CS_REST_NoSQL_API.Exceptions;
using MICHIPEDIA_CS_REST_NoSQL_API.Interfaces;
using MICHIPEDIA_CS_REST_NoSQL_API.Models;
using MongoDB.Driver;
using System.Data;

namespace MICHIPEDIA_CS_REST_NoSQL_API.Repositories
{
    public class PaisRepository(MongoDbContext unContexto) : IPaisRepository
    {
        private readonly MongoDbContext contextoDB = unContexto;

        public async Task<List<Pais>> GetAllAsync()
        {
            var conexion = contextoDB.CreateConnection();
            var coleccionPaises = conexion.GetCollection<Pais>(contextoDB.ConfiguracionColecciones.ColeccionPaises);

            var losPaises = await coleccionPaises
                .Find(_ => true)
                .SortBy(pais => pais.Continente)
                .ThenBy(pais => pais.Nombre)
                .ToListAsync();

            return losPaises;
        }

        public async Task<Pais> GetByIdAsync(string pais_id)
        {
            Pais unPais= new();

            var conexion = contextoDB.CreateConnection();
            var coleccionPaises = conexion.GetCollection<Pais>(contextoDB.ConfiguracionColecciones.ColeccionPaises);

            var resultado = await coleccionPaises
                .Find(pais => pais.Id == pais_id)
                .FirstOrDefaultAsync();

            if (resultado is not null)
                unPais = resultado;            

            return unPais;
        }

        public async Task<Pais> GetCountryByNameAndContinentAsync(Pais unPais)
        {
            Pais paisEncontrado = new();

            var conexion = contextoDB.CreateConnection();
            var coleccionPaises = conexion.GetCollection<Pais>(contextoDB.ConfiguracionColecciones.ColeccionPaises);

            var builder = Builders<Pais>.Filter;
            var filtro = builder.And(
                builder.Eq(pais => pais.Nombre, unPais.Nombre),
                builder.Eq(pais => pais.Continente, unPais.Continente));

            var resultado = await coleccionPaises
                .Find(filtro)
                .FirstOrDefaultAsync();

            if (resultado is not null)
                paisEncontrado = resultado;

            return paisEncontrado;
        }

        //public async Task<Pais> GetCountryByNameAndContinentAsync(string pais_continente)
        //{
        //    string[] datosPais = pais_continente.Split('-');

        //    Pais paisBuscado = new()
        //    {
        //        Nombre = datosPais[0].Trim(),
        //        Continente = datosPais[1].Trim()
        //    };

        //    var paisExistente = await GetCountryByNameAndContinentAsync(paisBuscado);

        //    return paisExistente;
        //}

        public async Task<string> GetContinentByNameAsync(string continente_nombre)
        {
            var conexion = contextoDB.CreateConnection();
            var coleccionPaises = conexion.GetCollection<Pais>(contextoDB.ConfiguracionColecciones.ColeccionPaises);

            FieldDefinition<Pais, string> campo = "continente";

            var continentesEcontrados = await coleccionPaises
                .DistinctAsync(campo, FilterDefinition<Pais>.Empty)
                .Result
                .ToListAsync();

            if (continentesEcontrados.Contains(continente_nombre))
                return continente_nombre;
            else
                return string.Empty;
        }


        //public async Task<int> GetTotalAssociatedBreedsByCountryGuidAsync(Guid pais_guid)
        //{
        //    var conexion = contextoDB.CreateConnection();

        //    DynamicParameters parametrosSentencia = new();
        //    parametrosSentencia.Add("@pais_guid", pais_guid,
        //                            DbType.Guid, ParameterDirection.Input);

        //    //Aqui colocamos la informacion nutricional
        //    string sentenciaSQL = "SELECT count(*) totalRegistros " +
        //        "FROM core.v_info_razas " +
        //        "WHERE pais_uuid = @pais_guid";

        //    var totalRegistros = await conexion
        //        .QueryAsync<int>(sentenciaSQL, parametrosSentencia);

        //    return totalRegistros.FirstOrDefault();
        //}


        public async Task<bool> CreateAsync(Pais unPais)
        {
            bool resultadoAccion = false;

            var conexion = contextoDB.CreateConnection();
            var coleccionPaises = conexion.GetCollection<Pais>(contextoDB.ConfiguracionColecciones.ColeccionPaises);

            await coleccionPaises
                .InsertOneAsync(unPais);

            var resultado = await GetCountryByNameAndContinentAsync(unPais);

            if (resultado is not null)
                resultadoAccion = true;

            return resultadoAccion;
        }

        //public async Task<bool> UpdateAsync(Pais unPais)
        //{
        //    bool resultadoAccion = false;

        //    var paisExistente = await GetByGuidAsync(unPais.Uuid);

        //    if (paisExistente.Uuid == Guid.Empty)
        //        throw new DbOperationException($"No se puede actualizar. No existe la fruta {unPais.Nombre!}.");

        //    try
        //    {
        //        var conexion = contextoDB.CreateConnection();

        //        string procedimiento = "core.p_actualizar_pais";
        //        var parametros = new
        //        {
        //            p_uuid = unPais.Uuid,
        //            p_nombre = unPais.Nombre,
        //            p_continente = unPais.Continente
        //        };

        //        var cantidad_filas = await conexion.ExecuteAsync(
        //            procedimiento,
        //            parametros,
        //            commandType: CommandType.StoredProcedure);

        //        if (cantidad_filas != 0)
        //            resultadoAccion = true;
        //    }
        //    catch (NpgsqlException error)
        //    {
        //        throw new DbOperationException(error.Message);
        //    }

        //    return resultadoAccion;
        //}

        //public async Task<bool> RemoveAsync(Guid pais_guid)
        //{
        //    bool resultadoAccion = false;

        //    try
        //    {

        //        var conexion = contextoDB.CreateConnection();

        //        string procedimiento = "core.p_eliminar_pais";
        //        var parametros = new
        //        {
        //            p_uuid = pais_guid
        //        };

        //        var cantidad_filas = await conexion.ExecuteAsync(
        //            procedimiento,
        //            parametros,
        //            commandType: CommandType.StoredProcedure);

        //        if (cantidad_filas != 0)
        //            resultadoAccion = true;
        //    }
        //    catch (NpgsqlException error)
        //    {
        //        throw new DbOperationException(error.Message);
        //    }

        //    return resultadoAccion;
        //}
    }
}
