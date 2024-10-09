using MICHIPEDIA_CS_REST_NoSQL_API.DbContexts;
using MICHIPEDIA_CS_REST_NoSQL_API.Interfaces;
using MICHIPEDIA_CS_REST_NoSQL_API.Models;

namespace MICHIPEDIA_CS_REST_NoSQL_API.Repositories
{
    public class ResumenRepository(MongoDbContext unContexto) : IResumenRepository
    {
        private readonly MongoDbContext contextoDB = unContexto;

        public async Task<Resumen> GetAllAsync()
        {
            Resumen unResumen = new();
            var conexion = contextoDB.CreateConnection();

            //Total Paises
            var coleccionPaises = conexion.GetCollection<Pais>(contextoDB.ConfiguracionColecciones.ColeccionPaises);
            var totalPaises = await coleccionPaises
                .EstimatedDocumentCountAsync();

            unResumen.Paises = totalPaises;

            //Total Razas
            var coleccionRazas = conexion.GetCollection<Raza>(contextoDB.ConfiguracionColecciones.ColeccionRazas);
            var totalRazas = await coleccionRazas
                .EstimatedDocumentCountAsync();

            unResumen.Razas = totalRazas;

            //Total Caracteristicas
            var coleccionCaracteristicas = conexion.GetCollection<Caracteristica>(contextoDB.ConfiguracionColecciones.ColeccionCaracteristicas);
            var totalCaracteristicas = await coleccionCaracteristicas
                .EstimatedDocumentCountAsync();

            unResumen.Caracteristicas = totalCaracteristicas;

            //Total Comportamientos
            var coleccionComportamientos = conexion.GetCollection<Comportamiento>(contextoDB.ConfiguracionColecciones.ColeccionComportamientos);
            var totalComportamientos = await coleccionComportamientos
                .EstimatedDocumentCountAsync();

            unResumen.Comportamientos = totalComportamientos;

            return unResumen;
        }
    }
}