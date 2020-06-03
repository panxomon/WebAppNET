using Web.Contexto.Entidades.Actividades;
using Web1.Models.Actividades;

namespace Web1.Mapper.Map
{
    public static class ActividadProfileMap
    {
        public static ActividadResult ToResult(this Actividad entidad)
        {
            var retorno = AutoMapper.Mapper.Map<Actividad, ActividadResult>(entidad);
            return retorno;
        }

        public static NuevaActividad ToCommand(this Actividad entidad)
        {
            var retorno = AutoMapper.Mapper.Map<Actividad, NuevaActividad>(entidad);
            return retorno;
        }

        public static Actividad ToEntity(this NuevaActividad entidad)
        {
            var retorno = AutoMapper.Mapper.Map<NuevaActividad, Actividad>(entidad);
            return retorno;
        }       
    }
}
