namespace Web.Base.Cqrs.Command
{
    public interface ICommandHandler<in TParameter> 
    {
        void Execute(TParameter command);
    }
}
