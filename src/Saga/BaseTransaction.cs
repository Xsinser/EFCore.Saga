namespace Saga;

public abstract class BaseTransaction : IDisposable, IAsyncDisposable
{
    private readonly CancellationTokenSource _cts = new();
    private bool _disposed;

    public BaseTransaction(IDisposable sourceTransaction)
    {
        SourceTransaction = sourceTransaction;
    }

    protected IDisposable SourceTransaction { get; private set; }

    public abstract void Commit();

    public abstract Task CommitAsync(CancellationToken cancellationToken = default);

    public abstract void Rollback();

    public abstract Task RollbackAsync(CancellationToken cancellationToken = default);

    #region IDisposable, IAsyncDisposable

    public virtual void Dispose()
    {
        if (_disposed) return;

        _cts.Cancel();
        _cts.Dispose();

        GC.SuppressFinalize(this);

        SourceTransaction.Dispose();

        _disposed = true;
    }

    public virtual async ValueTask DisposeAsync()
    {
        if (_disposed) return;

        _cts.Cancel();

        SourceTransaction.Dispose();

        _cts.Dispose();

        GC.SuppressFinalize(this);
        _disposed = true;
    }

    ~BaseTransaction() => Dispose();

    #endregion IDisposable, IAsyncDisposable
}