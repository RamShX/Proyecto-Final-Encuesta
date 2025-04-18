using AutoMapper;
using Domain.Dtos;
using Domain.Models;

namespace Domain.Mappers
{
    class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Usuario, UsuarioResponseDto>();
            //CreateMap<Usuario, UsuarioInfoBasicoDto>();

            CreateMap<UsuarioManagerDto, Usuario>()
                .ForMember(dest => dest.PasswordHash , opt => opt.Ignore())
                .ForMember(dest => dest.CreadoEn, opt => opt.Ignore())
                .ForMember(dest => dest.ActualizadoEn, opt => opt.Ignore());


            CreateMap<CrearEncuestaDto, Encuesta>()
                .ForMember(dest => dest.Creador, opt => opt.Ignore())  // Ignorar la propiedad Creador en Encuesta porque no lo puse en el DTO
                .ForMember(dest => dest.UsuarioId, opt => opt.MapFrom(src => src.UsuarioId));

            CreateMap<Encuesta, EncuestaRespuestaDto>()
                .ForMember(dest => dest.Creador, opt => opt.MapFrom(src => src.Creador));
            CreateMap<EncuestaUpdateDto, Encuesta>();




        }
    }
}
