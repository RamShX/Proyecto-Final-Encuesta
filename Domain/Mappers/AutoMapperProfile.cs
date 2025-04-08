using AutoMapper;
using Domain.Dtos;
using Domain.Models;

namespace Domain.Mappers
{
    class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Usuario, UsuarioRespuestaDto>();
               

        }
    }
}
