using AgenciaInmobiliaria_API.Dto;
using AgenciaInmobiliaria_API.Modelos;
using AutoMapper;

namespace AgenciaInmobiliaria_API
{
    public class MappingConfig : Profile
    {
        // Esta clase se utiliza para convertir un tipo de objeto a otro
        // o, mas bien, mapear. Ejemplo: convertir un objeto de tipo
        // inmueble a inmuebleDto o viceversa.
        public MappingConfig()
        {
            CreateMap<Inmuebles, InmueblesDto>();
            CreateMap<InmueblesDto,Inmuebles>();

            CreateMap<Inmuebles, InmueblesCreateDto>().ReverseMap();
            CreateMap<Inmuebles, InmueblesUpdateDto>().ReverseMap();
        }
    }
}
