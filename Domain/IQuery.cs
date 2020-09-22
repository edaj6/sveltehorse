namespace Domain
{
    public interface IQuery<TResult>
    {
    }

    public interface IQueryHandler<TQuery, out TResult> where TQuery : IQuery<TResult>
    {
        TResult Handle(TQuery query);
    }
}
