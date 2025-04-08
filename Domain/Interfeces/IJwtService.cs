using Domain.Dtos;
using Domain.Models;
using Google.Protobuf.WellKnownTypes;

namespace Domain.Interfeces
{
    public interface IJwtService
    {
       Task<string> GenerarToken(Usuario usuariO);
    }
}
