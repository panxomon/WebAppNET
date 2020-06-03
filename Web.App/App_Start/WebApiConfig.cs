using System.Web.Http;
using Web.App.Validate;
using Web.Base.Identity.Core;

namespace Web.App
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);

            config.Routes.MapHttpRoute(
              name: "Account",
              routeTemplate: "api/Accounnt/{mail}",
              defaults: new { mail = RouteParameter.Optional, controller = "Account" }
            );

            config.Routes.MapHttpRoute(
              name: "Usuario",
              routeTemplate: "api/Usuario/",
              defaults: new { controller = "Usuario" }
            );

            config.Routes.MapHttpRoute(
              name: "getUsuario",
              routeTemplate: "api/Usuario/{Id}",
              defaults: new { id = RouteParameter.Optional, controller = "Usuario" }
            );

            //Usuario
            config.Routes.MapHttpRoute(
              name: "getUsuarioByName",
              routeTemplate: "api/Usuario/{usuario}/{password}",
              defaults: new { usuario = RouteParameter.Optional, password = RouteParameter.Optional, controller = "Usuario" }
            );

            //Claims
            config.Routes.MapHttpRoute(
              name: "getClaims",
              routeTemplate: "api/Claims/{id}",
              defaults: new { id = RouteParameter.Optional, controller = "Claims" }
            );

            //Roles
            config.Routes.MapHttpRoute(
            name: "deleteRol",
            routeTemplate: "api/Roles/{userId}",
            defaults: new { userId = RouteParameter.Optional, controller = "Roles" }
            );

            //Roles
            config.Routes.MapHttpRoute(
            name: "Roles",
            routeTemplate: "api/Roles/",
            defaults: new { id = RouteParameter.Optional, controller = "Roles" }
            );

            config.Routes.MapHttpRoute(
                name: "getAccount",
                routeTemplate: "api/Account/{mail}",
                defaults: new { mail = RouteParameter.Optional, controller = "Account" }
              );


            config.Routes.MapHttpRoute(
               name: "getClaimsUser",
               routeTemplate: "api/Account/{id}/{tip}",
               defaults: new { tip = RouteParameter.Optional, id = RouteParameter.Optional, controller = "Account" }
             );

            //Login
            config.Routes.MapHttpRoute(
              name: "login",
              routeTemplate: "api/login/{Id}",
              defaults: new { id = RouteParameter.Optional, controller = "Login" }
            );

            //Login
            config.Routes.MapHttpRoute(
              name: "loginPost",
              routeTemplate: "api/login",
              defaults: new { controller = "Login" }
            );

            config.Routes.MapHttpRoute(
                name: "Persona",
                routeTemplate: "api/{controller}",
                defaults: new { controller = "Persona" }
            );

            // Quite los comentarios de la siguiente línea de código para habilitar la compatibilidad de consultas para las acciones con un tipo de valor devuelto IQueryable o IQueryable<T>.
            // Para evitar el procesamiento de consultas inesperadas o malintencionadas, use la configuración de validación en QueryableAttribute para validar las consultas entrantes.
            // Para obtener más información, visite http://go.microsoft.com/fwlink/?LinkId=279712.
            //config.EnableQuerySupport();
            //.Constraints["Namespaces"] = new string[] { "App.Controllers" };
            // Para deshabilitar el seguimiento en la aplicación, incluya un comentario o quite la siguiente línea de código
            // Para obtener más información, consulte: http://www.asp.net/web-api
            //config.EnableSystemDiagnosticsTracing();

            //var cacheConfig = config.CacheOutputConfiguration();
            //cacheConfig.RegisterCacheOutputProvider(() => new MemoryCacheDefault());
            // Optional - prevent validation errors reaching controller (no need to check ModelState.IsValid)
            //config.Filters.Add(new ValidationActionFilter());           // Prevents validation errors reaching controller

            config.Filters.Add(new ClaimsAuthorisationFilter());
            config.Filters.Add(new ModelStateFilter());


            //Configuracion para fluent validation
            config.MessageHandlers.Add(new ResponseWrappingHandler());

            config.Formatters.Remove(config.Formatters.XmlFormatter);
            var json = config.Formatters.JsonFormatter;
            json.Indent = true;
            GlobalConfiguration.Configuration.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;
        }
    }
}
