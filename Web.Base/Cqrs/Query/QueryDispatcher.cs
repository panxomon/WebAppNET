using System;
using Microsoft.Practices.Unity;
using Unity;

namespace Web.Base.Cqrs.Query
{
    public class QueryDispatcher : IQueryDispatcher
    {
        private IUnityContainer _container;

        public QueryDispatcher(IUnityContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("kernel");
            }
            _container = container;
        }

        public TResult Dispatch<TParameter, TResult>(TParameter query) where TParameter : IQuery where TResult : IQueryResult
        {
            var handler = _container.Resolve<IQueryHandler<TParameter, TResult>>();
            return handler.Retrieve(query);
        }
    }
}
