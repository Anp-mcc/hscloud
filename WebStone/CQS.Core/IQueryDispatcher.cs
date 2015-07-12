namespace CQS.Core
{
    public interface IQueryDispatcher
    {
        TResult Dispatch<TParameter, TResult>(TParameter query) where TParameter : IQuery where TResult : IQueryResult;
    }
}