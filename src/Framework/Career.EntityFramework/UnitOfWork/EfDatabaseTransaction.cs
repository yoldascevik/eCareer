using Career.Repositories.UnitOfWok;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Career.EntityFramework.UnitOfWork;

public class EfDatabaseTransaction: IDatabaseTransaction
{
    private bool _disposed;
    private readonly IDbContextTransaction _transaction;

    public EfDatabaseTransaction(DbContext context)
    {
        _transaction = context.Database.BeginTransaction();
    }
        
    public void Commit()
    {
        _transaction.Commit();
    }

    public async Task CommitAsync(CancellationToken cancellationToken = default)
    {
        await _transaction.CommitAsync(cancellationToken);
    }

    public void Rollback()
    {
        _transaction.Rollback();
    }

    public async Task RollbackAsync(CancellationToken cancellationToken = default)
    {
        await _transaction.RollbackAsync(cancellationToken);
    }

    #region IDisposable Members

    protected void Dispose(bool disposing)
    {
        if (!this._disposed)
        {
            if (disposing)
            {
                _transaction.Dispose();
            }
        }
        this._disposed = true;
    }

    protected async Task DisposeAsync(bool disposing)
    {
        if (!this._disposed)
        {
            if (disposing)
            {
                await _transaction.DisposeAsync();
            }
        }
        this._disposed = true;
    }
        
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
        
    public async ValueTask DisposeAsync()
    {
        await DisposeAsync(true);
        GC.SuppressFinalize(this);
    }

    #endregion
}