using Web1.Mapper.Profile;

namespace Web.App
{
    public static class AutoMapperConfig
    {
        public static void Configure()
        {
            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.AddProfile(new ActividadProfile());
            });
        }
    }
}