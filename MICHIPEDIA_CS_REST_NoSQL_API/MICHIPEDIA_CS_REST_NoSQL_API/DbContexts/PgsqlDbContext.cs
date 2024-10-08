using Npgsql;
using System.Data;

namespace MICHIPEDIA_CS_REST_SQL_API.DbContexts
{
    public class PgsqlDbContext(IConfiguration unaConfiguracion)
    {
        private readonly string cadenaConexion = unaConfiguracion.GetConnectionString("MichiPL")!;

        public IDbConnection CreateConnection()
        {
            return new NpgsqlConnection(cadenaConexion);
        }
    }
}