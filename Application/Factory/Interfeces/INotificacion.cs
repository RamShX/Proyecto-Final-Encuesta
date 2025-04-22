
using Domain.Dtos;

namespace Application.Factory.Interfeces
{
    public interface INotificacion
    {
        Task enviar(CrearNotificacionDto notificacionDto);
        string tipo { get; }
    }
}
