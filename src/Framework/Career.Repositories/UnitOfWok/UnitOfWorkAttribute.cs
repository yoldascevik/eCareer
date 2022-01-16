using System;
using System.Threading.Tasks;
using AspectCore;
using AspectCore.Aspects;
using Microsoft.Extensions.DependencyInjection;

namespace Career.Repositories.UnitOfWok;

/// <summary>
/// Atomic transaction for methods.
/// </summary>
[AttributeUsage(AttributeTargets.Method)]
public class UnitOfWorkAttribute : AspectAttribute
{
    private IUnitOfWork _unitOfWork;
    private IDatabaseTransaction _transaction;
        
    public override AspectAttribute LoadDependencies(IServiceProvider serviceProvider)
    {
        _unitOfWork ??= serviceProvider.GetRequiredService<IUnitOfWork>();
        return base.LoadDependencies(serviceProvider);
    }

    public override void OnBefore(MethodExecutionArgs args) => _transaction = _unitOfWork.BeginTransaction();
        
    public override async Task OnBeforeAsync(MethodExecutionArgs args) => _transaction = await _unitOfWork.BeginTransactionAsync();
        
    public override void OnSuccess(MethodExecutionArgs args) => _transaction.Commit();

    public override async Task OnSuccessAsync(MethodExecutionArgs args) => await _transaction.CommitAsync();

    public override void OnException(MethodExecutionArgs args) => _transaction.Rollback();

    public override async Task OnExceptionAsync(MethodExecutionArgs args) => await _transaction.RollbackAsync();
}