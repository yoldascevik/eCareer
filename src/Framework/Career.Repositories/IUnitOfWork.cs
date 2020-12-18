using System;
using System.Threading;
using System.Threading.Tasks;

namespace Career.Repositories
{
    // ReSharper disable once UnusedTypeParameter
    public interface IUnitOfWork<TContext> : IUnitOfWork
    {
    }

    public interface IUnitOfWork : IDisposable, IAsyncDisposable
    {
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        IDatabaseTransaction BeginTransaction();
        Task<IDatabaseTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
    }
}