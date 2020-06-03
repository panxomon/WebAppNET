using System;
using Unity;
using Unity.Injection;
using Unity.Lifetime;
using Web.Base.Cqrs.Command;
using Web.Base.Cqrs.Query;
using Web.Base.Identity.Core;
using Web.Contexto;

namespace Web.App
{
    public static class UnityConfig
    {        
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() => {var container = new UnityContainer();RegisterTypes(container); return container;});

        public static IUnityContainer Container => container.Value;

        public static void RegisterTypes(IUnityContainer container)
        {
            var conexionSqlServer = "name=conexion";
            var conexionIdentity = "Name=Identity";

            container.RegisterType<WebContext>(new InjectionFactory(c => new WebContext(conexionSqlServer)));
            container.RegisterType<ApiUserContext>(new InjectionFactory(c => new ApiUserContext(conexionIdentity)));

            container.RegisterType<OmsUserManager>();

            container.RegisterType<RoleManager>(new TransientLifetimeManager());
            container.RegisterType<ClaimedActionsProvider>(new TransientLifetimeManager());
            container.RegisterType<SignInManager>(new TransientLifetimeManager());
            container.RegisterType<ClaimsFactory>(new TransientLifetimeManager());
            container.RegisterType<UserValidator>(new TransientLifetimeManager());

            container.RegisterType<ICommandDispatcher, CommandDispatcher>(new TransientLifetimeManager());
            container.RegisterType<IQueryDispatcher, QueryDispatcher>(new TransientLifetimeManager());

         
        }
    }
}