﻿using MICHIPEDIA_CS_REST_NoSQL_API.DbContexts;
using MICHIPEDIA_CS_REST_NoSQL_API.Interfaces;
using MICHIPEDIA_CS_REST_NoSQL_API.Models;
using MongoDB.Driver;
using System.Data;

namespace MICHIPEDIA_CS_REST_NoSQL_API.Repositories
{
    public class CaracteristicaRepository(MongoDbContext unContexto) : ICaracteristicaRepository
    {
        private readonly MongoDbContext contextoDB = unContexto;

        public async Task<List<Caracteristica>> GetAllAsync()
        {
            var conexion = contextoDB.CreateConnection();
            var coleccionCaracteristicas = conexion.GetCollection<Caracteristica>(contextoDB.ConfiguracionColecciones.ColeccionCaracteristicas);

            var lasCaracteristicas = await coleccionCaracteristicas
                .Find(_ => true)
                .SortBy(caracteristica => caracteristica.Nombre)
                .ToListAsync();

            return lasCaracteristicas;
        }

        public async Task<Caracteristica> GetByIdAsync(string caracteristica_id)
        {
            Caracteristica unaCaracteristica = new();

            var conexion = contextoDB.CreateConnection();
            var coleccionCaracteristicas = conexion.GetCollection<Caracteristica>(contextoDB.ConfiguracionColecciones.ColeccionCaracteristicas);

            var resultado = await coleccionCaracteristicas
                .Find(caracteristica => caracteristica.Id == caracteristica_id)
                .FirstOrDefaultAsync();

            if (resultado is not null)
                unaCaracteristica = resultado;

            return unaCaracteristica;
        }

        //public async Task<CaracteristicaValorada> GetDetailedCharacteristicByGuidAsync(Guid caracteristica_guid)
        //{
        //    Caracteristica unaCaracteristica = await GetByGuidAsync(caracteristica_guid);

        //    CaracteristicaValorada unaCaracteristicaValorada = new()
        //    {
        //        Uuid = unaCaracteristica.Uuid,
        //        Nombre = unaCaracteristica.Nombre,
        //        Descripcion = unaCaracteristica.Descripcion,
        //        Valoracion_Caracteristicas = await GetValuedCharacteristicByGuidAsync(caracteristica_guid)
        //    };

        //    return unaCaracteristicaValorada;
        //}

        //private async Task<List<CaracteristicaRaza>> GetValuedCharacteristicByGuidAsync(Guid caracteristica_guid)
        //{
        //    List<CaracteristicaRaza> infoCaracteristicasValoradas = [];

        //    var conexion = contextoDB.CreateConnection();

        //    DynamicParameters parametrosSentencia = new();
        //    parametrosSentencia.Add("@caracteristica_uuid", caracteristica_guid,
        //                            DbType.Guid, ParameterDirection.Input);

        //    string sentenciaSQL =
        //        "SELECT DISTINCT raza_uuid, raza_nombre, " +
        //        "caracteristica_valoracion valoracion " +
        //        "FROM v_info_caracteristicas_razas " +
        //        "WHERE caracteristica_uuid = @caracteristica_uuid " +
        //        "ORDER BY raza_nombre";

        //    var resultado = await conexion
        //        .QueryAsync<CaracteristicaRaza>(sentenciaSQL, parametrosSentencia);

        //    if (resultado.Any())
        //        infoCaracteristicasValoradas = resultado.ToList();

        //    return infoCaracteristicasValoradas;
        //}
    }
}
