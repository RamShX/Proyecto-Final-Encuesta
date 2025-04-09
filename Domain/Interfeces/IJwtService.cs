
using Domain.Models;

namespace Domain.Interfeces
{
    public interface IJwtService
    {
       Task<string> GenerarToken(Usuario usuariO);
    }
}
