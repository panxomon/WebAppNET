using System;
using System.Configuration;
using System.Threading.Tasks;
using System.Web.Cors;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using FluentValidation.WebApi;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Owin;
using Swashbuckle.Application;
using Unity.AspNet.WebApi;
using Web.App;
using Web.App.Providers;
using Web.App.Providers.OAuthPrivider;
using Web.App.Unity.Resolver;

[assembly: OwinStartup(typeof (Startup))]
namespace Web.App
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {         
            var container = UnityConfig.Container;
         
            var config = new HttpConfiguration
            {
                DependencyResolver = new UnityDependencyResolver(container)
            };            
            
            config.Services.Replace(typeof(IHttpControllerActivator), new ControllerActivator());

            FluentValidationModelValidatorProvider.Configure(config);

            WebApiConfig.Register(config);                             

            app.UseUnityContainerPerRequest(container);

            ConfigureOAuth(app);

            CrearCors(app);

            ConfigureSwagger(config);

            AutoMapperConfig.Configure();

            app.UseWebApi(config);
        }

        public void CrearCors(IAppBuilder app)
        {
            var policy = new CorsPolicy
            {
                AllowAnyHeader = true,
                AllowAnyMethod = true,
                AllowAnyOrigin = true,
                SupportsCredentials = true
            };

            policy.ExposedHeaders.Add("X-Custom-Header");

            app.UseCors(new CorsOptions
            {
                PolicyProvider = new CorsPolicyProvider
                {
                    PolicyResolver = Context => Task.FromResult(policy)
                }
            });
        }

        public void ConfigureOAuth(IAppBuilder app)
        {
            var tiempoExpiracionToken = Convert.ToDouble(ConfigurationManager.AppSettings["TiempoTokenHoras"]);

            var oAuthServerOptions = new OAuthAuthorizationServerOptions
            {
                AuthenticationType = OAuthDefaults.AuthenticationType,
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/authorize"),
                AccessTokenExpireTimeSpan = TimeSpan.FromHours(tiempoExpiracionToken),
                Provider = new OAuthProvider()
            };

            var authOptions = new OAuthBearerAuthenticationOptions
            {
                AuthenticationMode = AuthenticationMode.Active
            };

            app.UseOAuthBearerAuthentication(authOptions);
            app.UseOAuthAuthorizationServer(oAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }

        public void ConfigureSwagger(HttpConfiguration config)
        {
            //GlobalConfiguration.Configure(WebApiConfig.Register);

            GlobalConfiguration.Configuration
             .EnableSwagger(c =>
             {
                 c.SingleApiVersion("v1", "SomosTechies API")
                  .Description("A sample API for testing")
                  .TermsOfService("Some terms");
             })
         .EnableSwaggerUi();
        }

    }
}