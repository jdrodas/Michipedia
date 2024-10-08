/*
DbOperationException:
Excepcion creada para enviar mensajes relacionados 
con la ejecución de operaciones CRUD en la base de datos
*/

namespace MICHIPEDIA_CS_REST_SQL_API.Exceptions
{
    public class DbOperationException(string message) : Exception(message)
    {
    }
}
