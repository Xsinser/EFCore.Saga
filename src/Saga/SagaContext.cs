using System.Collections.Concurrent;

namespace Saga;

public class SagaContext : IDisposable, IAsyncDisposable
{
    private readonly CancellationTokenSource _cts = new();
    private bool _disposed = false;
    private bool _cachedLayers = false;
    protected ConcurrentStack<BaseTransaction> Transactions = [];

    public bool CachedLayers { get => _cachedLayers; }

    public SagaContext()
    { }

    public SagaContext(bool cahcedLayers)
    {
        _cachedLayers = cahcedLayers;
    }

    public SagaContext(SagaContext context)
    {
        Transactions = context.Transactions;
        _cachedLayers = context._cachedLayers;
        _cts = context._cts;
    }

    public void WriteSaga(BaseTransaction transaction) => Transactions.Push(transaction);

    public void Commit()
    {
        foreach (var transaction in Transactions)
            transaction.Commit();
    }

    public async Task CommitAsync(CancellationToken cancellationToken = default)

    {
        foreach (var transaction in Transactions)
            await transaction.CommitAsync(cancellationToken);
    }

    public void Rollback()
    {
        foreach (var transaction in Transactions)
            transaction.Rollback();
    }

    public async Task RollbackAsync(CancellationToken cancellationToken = default)
    {
        foreach (var transaction in Transactions)
            await transaction.RollbackAsync(cancellationToken);
    }

    #region IDisposable, IAsyncDisposable

    public void Dispose()
    {
        if (_disposed) return;

        _cts.Cancel();
        _cts.Dispose();

        foreach (var transaction in Transactions)
            transaction.Dispose();

        GC.SuppressFinalize(this);
        _disposed = true;
    }

    public async ValueTask DisposeAsync()
    {
        if (_disposed) return;

        _cts.Cancel();

        foreach (var transaction in Transactions)
            await transaction.DisposeAsync();

        _cts.Dispose();

        GC.SuppressFinalize(this);
        _disposed = true;
    }

    ~SagaContext() => Dispose();

    #endregion IDisposable, IAsyncDisposable
}