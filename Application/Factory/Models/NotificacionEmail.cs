

using Application.Factory.Interfeces;
using Domain.Dtos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Mail;

namespace Application.Factory.Models
{
    public class NotificacionEmail : INotificacion
    {
        public string tipo => "email";
        private readonly IConfiguration _confi;
        private ILogger<NotificacionEmail> _logger;

        public NotificacionEmail(IConfiguration configuration, ILogger<NotificacionEmail>logger)
        {
            _confi = configuration;
            _logger = logger;
        }

        public async Task enviar(CrearNotificacionDto notificacionDto)
        {
            try
            {
                // conf de SMTP de appsetting
                var smtpSettings = _confi.GetSection("SmtpSettings");
                var host = smtpSettings["Host"];
                var port = int.Parse(smtpSettings["Port"]);
                var userName = smtpSettings["UserName"];
                var enableSsl = bool.Parse(smtpSettings["EnableSsl"]);
                var password = smtpSettings["Password"];
                var fromEmail = smtpSettings["FromEmail"];

                var ToEmail = smtpSettings["FromEmail"];

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(fromEmail),
                    Subject = $"Notificación: {notificacionDto.Tipo}",
                    Body = notificacionDto.Mensaje,
                    IsBodyHtml = true
                };

                mailMessage.To.Add(ToEmail);

                // Configuración del cliente SMTP
                using (var client = new SmtpClient(host, port))
                {
                    client.EnableSsl = enableSsl;
                    client.Credentials = new NetworkCredential(userName, password);
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;

                    // Enviar email de forma asíncrona
                    await client.SendMailAsync(mailMessage);
                    _logger.LogInformation($"Email enviado a {ToEmail}: {notificacionDto.Mensaje}");
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error al enviar notificación por email: {e.Message}");
                throw;
            }
        }
    }
}
