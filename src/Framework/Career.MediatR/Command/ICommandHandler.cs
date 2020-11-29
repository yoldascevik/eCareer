using MediatR;

namespace Career.MediatR.Command
{
    public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand> 
        where TCommand : ICommand
    {

    }

    public interface ICommandHandler<in TCommand, TResult> : IRequestHandler<TCommand, TResult>
        where TCommand : ICommand<TResult>
    {

    }
}