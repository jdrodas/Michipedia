
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

        public async Task<List<ComportamientoSimplificado>> GetBehaviorsByIdAsync(string raza_id)
        {
            List<ComportamientoSimplificado> comportamientosAsociados = [];

            var conexion = contextoDB.CreateConnection();
            var coleccionComportamientosRazas = conexion.GetCollection<ComportamientoRaza>(contextoDB.ConfiguracionColecciones.ColeccionComportamientosRazas);

            var losComportamientos = await coleccionComportamientosRazas
                .Find(comportamientoRaza=> comportamientoRaza.RazaId == raza_id)
                .ToListAsync();

            Comportamiento unComportamientoBuscado;
            var coleccionComportamientos = conexion.GetCollection<Comportamiento>(contextoDB.ConfiguracionColecciones.ColeccionComportamientos);

            foreach (ComportamientoRaza unComportamiento in losComportamientos)
            {
                unComportamientoBuscado = await coleccionComportamientos
                    .Find(comportamiento => comportamiento.Id == unComportamiento.ComportamientoId!)
                    .FirstOrDefaultAsync();

                comportamientosAsociados.Add(
                    new ComportamientoSimplificado()
                    {
                        Nombre = unComportamientoBuscado.Nombre,
                        Descripcion = unComportamientoBuscado.Descripcion,
                        Nivel = unComportamiento.Nivel,
                        Valoracion = unComportamiento.Valoracion
                    }
                );
            }
            return comportamientosAsociados;
        }
    }
}
