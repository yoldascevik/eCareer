using MediatR;

namespace Career.MediatR.Query
{
    public interface IQueryHandler<in TQuery, TResult> : IRequestHandler<TQuery, TResult> 
        where TQuery : IQuery<TResult>
    {

    }
}