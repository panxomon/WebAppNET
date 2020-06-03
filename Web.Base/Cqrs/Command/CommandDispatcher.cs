using System;
using System.Windows;
using Microsoft.Practices.Unity;
using Unity;

namespace Web.Base.Cqrs.Command
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private IUnityContainer _container;

        public CommandDispatcher(IUnityContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }
            _container = container;
        } 

        public void Dispatch<TParameter>(TParameter command) where TParameter : ICommand
        {
            var handler = _container.Resolve<ICommandHandler<TParameter>>();

            handler.Execute(command);
        }
    }
}
