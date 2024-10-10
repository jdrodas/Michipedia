namespace MICHIPEDIA_CS_REST_NoSQL_API.Models
{
    public class MichisDatabaseSettings
    {
        public string DatabaseName { get; set; } = null!;
        public string ColeccionRazas { get; set; } = null!;
        public string ColeccionPaises { get; set; } = null!;
        public string ColeccionCaracteristicas { get; set; } = null!;
        public string ColeccionComportamientos { get; set; } = null!;
        public string ColeccionContinentes { get; set; } = null!;

        public MichisDatabaseSettings(IConfiguration unaConfiguracion)
        {
            var configuracion = unaConfiguracion.GetSection("MichisDatabaseSettings");

            DatabaseName = configuracion.GetSection("DatabaseName").Value!;
            ColeccionRazas = configuracion.GetSection("ColeccionRazas").Value!;
            ColeccionPaises = configuracion.GetSection("ColeccionPaises").Value!;
            ColeccionCaracteristicas = configuracion.GetSection("ColeccionCaracteristicas").Value!;
            ColeccionComportamientos = configuracion.GetSection("ColeccionComportamientos").Value!;
            ColeccionContinentes = configuracion.GetSection("ColeccionContinentes").Value!;

        }
    }
}