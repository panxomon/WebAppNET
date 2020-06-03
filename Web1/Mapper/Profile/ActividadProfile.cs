using Web.Contexto.Entidades.Actividades;
using Web1.Models.Actividades;

namespace Web1.Mapper.Profile
{
    public class ActividadProfile : AutoMapper.Profile
    {
        public ActividadProfile()
        {
            CreateMap<Actividad, NuevaActividad>()
               .ForMember(x => x.Id, y => y.MapFrom(m => m.ActividadId))
               .ForMember(x => x.Nombre, y => y.MapFrom(m => m.Nombre))
               .ForMember(x => x.Codigo, y => y.MapFrom(m => m.Codigo))
               .ReverseMap();

            CreateMap<Actividad, ActividadResult>()
               .ForMember(x => x.Id, y => y.MapFrom(m => m.ActividadId))
               .ForMember(x => x.Nombre, y => y.MapFrom(m => m.Nombre))
               .ForMember(x => x.Codigo, y => y.MapFrom(m => m.Codigo))
               .ReverseMap();
        }
    }
}
