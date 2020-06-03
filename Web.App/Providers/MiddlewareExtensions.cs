using Owin;
using Unity;
using Web.App.Unity;
using Web.App.Unity.Resolver;

namespace Web.App.Providers
{
    public static class MiddlewareExtensions
    {
        public static IAppBuilder UseCustomMiddleware(this IAppBuilder app)
        {
            app.Use(typeof(InjectionMiddleware));
            return app;
        }

        public static IAppBuilder UseUnityContainerPerRequest(this IAppBuilder app, IUnityContainer container)
        {
            app.Use(typeof(UnityContainerPerRequestMiddleware), container);
            return app;
        }
    }
}