using MediatR;

namespace Career.MediatR.Query;

public interface IQuery<out TResult> : IRequest<TResult>
{

}