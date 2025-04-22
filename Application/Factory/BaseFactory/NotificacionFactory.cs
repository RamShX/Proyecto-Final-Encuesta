

using Application.Factory.Interfeces;
using Application.Factory.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Application.Factory.BaseFactory
{
    public class NotificacionFactory
    {
        public static INotificacion CrearNotificacion(string tipo, ILogger<NotificacionEmail>logger, IConfiguration configuration) 
        {
            switch (tipo.ToLower())
            {
                case "email":
                    return new NotificacionEmail(configuration, logger);
                default:
                    throw new ArgumentException("Tipo de notificación no soportado");
            }
        }
    }
}
