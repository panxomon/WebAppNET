using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Unity;
using Web.App.Providers;

namespace Web.App.Unity.Resolver
{
    public class UnityContainerPerRequestMiddleware : OwinMiddleware
    {
        public UnityContainerPerRequestMiddleware(OwinMiddleware next, IUnityContainer container) : base(next)
        {
            _next = next;
            _container = container;
        }

        public override async Task Invoke(IOwinContext context)
        {            
            var childContainer = _container.CreateChildContainer();
            
            context.Set(HttpApplicationKey.OwinPerRequestUnityContainerKey, childContainer);

            await _next.Invoke(context);
            
            childContainer.Dispose();
        }

        private readonly OwinMiddleware _next;
        private readonly IUnityContainer _container;
    }
}