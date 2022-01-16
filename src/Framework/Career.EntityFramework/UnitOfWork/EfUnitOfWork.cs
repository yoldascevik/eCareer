using Career.Repositories.UnitOfWok;
using Microsoft.EntityFrameworkCore;

namespace Career.EntityFramework.UnitOfWork;

public class EfUnitOfWork<TContext>: IUnitOfWork<TContext>
    where TContext: DbContext
{
    private bool _disposed;
    private readonly TContext _context;

    public EfUnitOfWork(TContext context)
    {
        _context = context;
    }

    public int SaveChanges()
    {
        return _context.SaveChanges();
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public IDatabaseTransaction BeginTransaction()
    {
        return new EfDatabaseTransaction(_context);
    }

    public async Task<IDatabaseTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        return await Task.FromResult(new EfDatabaseTransaction(_context));
    }
        
    #region IDisposable Members

    protected void Dispose(bool disposing)
    {
        if (!this._disposed)
        {
            if (disposing)
            {
                _context.Dispose();
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
                await _context.DisposeAsync();
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