namespace Web.Base.Cqrs.Query
{
    public interface IQueryHandler<in TParameter, out TResult>
    {
        TResult Retrieve(TParameter query);
    }
}
