using MediatR;

namespace Career.MediatR.Command;

public interface ICommand: IRequest
{
        
}

public interface ICommand<out TResponse> : IRequest<TResponse>
{
        
}