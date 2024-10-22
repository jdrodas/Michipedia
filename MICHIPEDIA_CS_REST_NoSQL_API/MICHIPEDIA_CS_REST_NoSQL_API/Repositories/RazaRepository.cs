
using MICHIPEDIA_CS_REST_NoSQL_API.DbContexts;
using MICHIPEDIA_CS_REST_NoSQL_API.Exceptions;
using MICHIPEDIA_CS_REST_NoSQL_API.Interfaces;
using MICHIPEDIA_CS_REST_NoSQL_API.Models;
using MongoDB.Driver;
using System.Data;

namespace MICHIPEDIA_CS_REST_NoSQL_API.Repositories
{
    public class RazaRepository(MongoDbContext unContexto) : IRazaRepository
    {
        private readonly MongoDbContext contextoDB = unContexto;

        public async Task<List<Raza>> GetAllAsync()
        {
            var conexion = contextoDB.CreateConnection();
            var coleccionRazas = conexion.GetCollection<Raza>(contextoDB.ConfiguracionColecciones.ColeccionRazas);

            var lasRazas = await coleccionRazas
                .Find(_ => true)
                .SortBy(raza => raza.Nombre)
                .ToListAsync();

            return lasRazas;
        }

        public async Task<Raza> GetByIdAsync(string raza_id)
        {
            Raza unaRaza = new();

            var conexion = contextoDB.CreateConnection();
            var coleccionRazas = conexion.GetCollection<Raza>(contextoDB.ConfiguracionColecciones.ColeccionRazas);

            var resultado = await coleccionRazas
                .Find(raza => raza.Id == raza_id)
                .FirstOrDefaultAsync();

            if (resultado is not null)
                unaRaza = resultado;

            return unaRaza;
        }

        //public async Task<Raza> GetByNameAsync(string raza_nombre)
        //{
        //    Raza unaRaza = new();

        //    var conexion = contextoDB.CreateConnection();

        //    DynamicParameters parametrosSentencia = new();
        //    parametrosSentencia.Add("@raza_nombre", raza_nombre,
        //                            DbType.String, ParameterDirection.Input);

        //    string sentenciaSQL =
        //        "SELECT DISTINCT raza_uuid uuid, nombre, descripcion, pais " +
        //        "FROM v_info_razas " +
        //        "WHERE nombre = @raza_nombre ";


        //    var resultado = await conexion.QueryAsync<Raza>(sentenciaSQL,
        //        parametrosSentencia);

        //    if (resultado.Any())
        //        unaRaza = resultado.First();

        //    return unaRaza;
        //}

        public async Task<List<Raza>> GetByCountryAsync(string descripcion_pais)
        {
            List<Raza> razasAsociadas = [];

            var conexion = contextoDB.CreateConnection();
            var coleccionRazas = conexion.GetCollection<Raza>(contextoDB.ConfiguracionColecciones.ColeccionRazas);

            var lasRazas = await coleccionRazas
                .Find(raza => raza.Pais == descripcion_pais)
                .SortBy(raza => raza.Nombre)
                .ToListAsync();

            if (lasRazas.Any())
                razasAsociadas = lasRazas;

            return razasAsociadas;
        }

        public async Task<List<CaracteristicaSimplificada>> GetCharacteristicsByIdAsync(string raza_id)
        {
            List<CaracteristicaSimplificada> caracteristicasAsociadas = [];

            var conexion = contextoDB.CreateConnection();
            var coleccionCaracteristicasRazas = conexion.GetCollection<CaracteristicaRaza>(contextoDB.ConfiguracionColecciones.ColeccionCaracteristicasRazas);

            var lasCaracteristicas = await coleccionCaracteristicasRazas
                .Find(caracteristicaRaza => caracteristicaRaza.RazaId == raza_id)
                .ToListAsync();

            Caracteristica unaCaracteristicaBuscada;
            var coleccionCaracteristicas = conexion.GetCollection<Caracteristica>(contextoDB.ConfiguracionColecciones.ColeccionCaracteristicas);

            foreach (CaracteristicaRaza unaCaracteristica in lasCaracteristicas)
            {
                unaCaracteristicaBuscada = await coleccionCaracteristicas
                    .Find(caracteristica => caracteristica.Id == unaCaracteristica.CaracteristicaId!)
                    .FirstOrDefaultAsync();

                caracteristicasAsociadas.Add(
                    new CaracteristicaSimplificada()
                    {
                        Nombre = unaCaracteristicaBuscada.Nombre,
                        Descripcion = unaCaracteristicaBuscada.Descripcion,
                        Valoracion = unaCaracteristica.Valoracion
                    }
                );
            }
            return caracteristicasAsociadas;
        }

        //public async Task<RazaCaracterizada> GetCharacterizedBreedByGuidAsync(Guid raza_guid)
        //{
        //    Raza unaRaza = await GetByGuidAsync(raza_guid);

        //    RazaCaracterizada unaRazaCaracterizada = new()
        //    {
        //        Uuid = unaRaza.Uuid,
        //        Nombre = unaRaza.Nombre,
        //        Descripcion = unaRaza.Descripcion,
        //        Pais = unaRaza.Pais,
        //        Caracteristicas = await GetCharacteristicsDetailsAsync(raza_guid)
        //    };

        //    return unaRazaCaracterizada;
        //}

        //public async Task<RazaDetallada> GetDetailedBreedByGuidAsync(Guid raza_guid)
        //{
        //    Raza unaRaza = await GetByGuidAsync(raza_guid);

        //    RazaDetallada unaRazaDetallada = new()
        //    {
        //        Uuid = unaRaza.Uuid,
        //        Nombre = unaRaza.Nombre,
        //        Descripcion = unaRaza.Descripcion,
        //        Pais = unaRaza.Pais,
        //        Caracteristicas = await GetCharacteristicsDetailsAsync(raza_guid),
        //        Comportamientos = await GetBehaviorsDetailsAsync(raza_guid)
        //    };

        //    return unaRazaDetallada;
        //}

        //public async Task<bool> CreateAsync(Raza unaRaza)
        //{
        //    bool resultadoAccion = false;

        //    //Buscar el pais

        //    string[] datosPais = unaRaza.Pais!.Split('-');

        //    Pais paisBuscado = new()
        //    {
        //        Nombre = datosPais[0].Trim(),
        //        Continente = datosPais[1].Trim()
        //    };

        //    var conexion = contextoDB.CreateConnection();

        //    DynamicParameters parametrosSentencia = new();
        //    parametrosSentencia.Add("@pais_nombre", paisBuscado.Nombre,
        //                            DbType.String, ParameterDirection.Input);
        //    parametrosSentencia.Add("@pais_continente", paisBuscado.Continente,
        //                            DbType.String, ParameterDirection.Input);

        //    string sentenciaSQL =
        //        "SELECT id " +
        //        "FROM core.paises " +
        //        "WHERE LOWER(nombre) = LOWER(@pais_nombre) " +
        //        "AND LOWER(continente) = LOWER(@pais_continente)";

        //    var resultado = await conexion.QueryAsync<int>(sentenciaSQL,
        //        parametrosSentencia);

        //    int pais_id = resultado.FirstOrDefault();

        //    try
        //    {
        //        string procedimiento = "core.p_insertar_raza";

        //        var parametros = new
        //        {
        //            p_nombre = unaRaza.Nombre,
        //            p_pais_id = pais_id,
        //            p_descripcion = unaRaza.Descripcion
        //        };

        //        var cantidadFilas = await conexion
        //            .ExecuteAsync(
        //                procedimiento,
        //                parametros,
        //                commandType: CommandType.StoredProcedure);

        //        if (cantidadFilas != 0)
        //            resultadoAccion = true;
        //    }
        //    catch (NpgsqlException error)
        //    {
        //        throw new DbOperationException(error.Message);
        //    }

        //    return resultadoAccion;
        //}

        //private async Task<List<CaracteristicaSimplificada>> GetCharacteristicsDetailsAsync(Guid raza_guid)
        //{
        //    List<CaracteristicaSimplificada> infoCaracteristicas = [];

        //    var conexion = contextoDB.CreateConnection();

        //    DynamicParameters parametrosSentencia = new();
        //    parametrosSentencia.Add("@raza_guid", raza_guid,
        //                            DbType.Guid, ParameterDirection.Input);

        //    string sentenciaSQL =
        //        "SELECT DISTINCT caracteristica_nombre nombre, caracteristica_descripcion descripcion, " +
        //        "caracteristica_valoracion valoracion " +
        //        "FROM v_info_caracteristicas_razas " +
        //        "WHERE raza_uuid = @raza_guid " +
        //        "ORDER BY caracteristica_nombre ";

        //    var resultado = await conexion
        //        .QueryAsync<CaracteristicaSimplificada>(sentenciaSQL, parametrosSentencia);

        //    if (resultado.Any())
        //        infoCaracteristicas = resultado.ToList();

        //    return infoCaracteristicas;
        //}

        //private async Task<List<Comportamiento>> GetBehaviorsDetailsAsync(Guid raza_guid)
        //{
        //    List<Comportamiento> infoComportamientos = [];

        //    var conexion = contextoDB.CreateConnection();

        //    DynamicParameters parametrosSentencia = new();
        //    parametrosSentencia.Add("@raza_guid", raza_guid,
        //                            DbType.Guid, ParameterDirection.Input);

        //    string sentenciaSQL =
        //        "SELECT DISTINCT comportamiento_nombre nombre, " +
        //        "comportamiento_descripcion descripcion, " +
        //        "nivel_nombre nivel, " +
        //        "nivel_valoracion valoracion " +
        //        "FROM v_info_comportamientos_razas " +
        //        "WHERE raza_uuid = @raza_guid " +
        //        "ORDER BY comportamiento_nombre";

        //    var resultado = await conexion
        //        .QueryAsync<Comportamiento>(sentenciaSQL, parametrosSentencia);

        //    if (resultado.Any())
        //        infoComportamientos = resultado.ToList();

        //    return infoComportamientos;
        //}
    }
}
