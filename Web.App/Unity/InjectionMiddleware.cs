using Microsoft.Owin;
using System.Threading.Tasks;
using Unity;
using Web.App.Providers;

namespace Web.App.Unity
{
    public class InjectionMiddleware : OwinMiddleware
    {
        public InjectionMiddleware(OwinMiddleware next) : base(next)
        {
            _next = next; 
        }

        public override async Task Invoke(IOwinContext context)
        {
            // Get container that we set to OwinContext using common key
            var container = context.Get<IUnityContainer>(HttpApplicationKey.OwinPerRequestUnityContainerKey);

            // Resolve registered services
            //var sameInARequest = container.Resolve<SameInARequest>();

            await _next.Invoke(context);
        }

        private readonly OwinMiddleware _next;
    }
}