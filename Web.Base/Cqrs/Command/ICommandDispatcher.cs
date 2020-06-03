namespace Web.Base.Cqrs.Command
{
    public interface ICommandDispatcher
    {
        void Dispatch<TParameter>(TParameter command) where TParameter : ICommand;               
    }
}
