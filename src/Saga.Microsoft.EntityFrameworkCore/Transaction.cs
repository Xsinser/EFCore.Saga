using Microsoft.EntityFrameworkCore.Storage;

namespace Saga.Microsoft.EntityFrameworkCore;

public class Transaction : BaseTransaction
{
    private IDbContextTransaction _transaction;

    public Transaction(SagaLayer sagaLayer, IDbContextTransaction transaction) : base(sagaLayer)
    {
        _transaction = transaction;
    }

    public override void Commit() => _transaction.Commit();

    public override async Task CommitAsync(CancellationToken cancellationToken = default) => await _transaction.CommitAsync(cancellationToken);

    public override void Rollback() => _transaction.Rollback();

    public override async Task RollbackAsync(CancellationToken cancellationToken = default) => await _transaction.RollbackAsync(cancellationToken);

    #region IDisposable, IAsyncDisposable

    public override void Dispose()
    {
        _transaction.Dispose();
        base.Dispose();
    }

    public override async ValueTask DisposeAsync()
    {
        await _transaction.DisposeAsync();
        await base.DisposeAsync();
    }

    ~Transaction() => Dispose();

    #endregion IDisposable, IAsyncDisposable
}