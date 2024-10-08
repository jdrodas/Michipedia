/*
AppValidationException:
Excepcion creada para enviar mensajes relacionados 
con la validación de datos en las capas de servicio
*/

namespace MICHIPEDIA_CS_REST_NoSQL_API.Exceptions
{
    public class AppValidationException(string message) : Exception(message)
    {
    }
}
