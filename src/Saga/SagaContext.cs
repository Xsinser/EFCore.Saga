namespace Saga;

public class SagaContext : IDisposable, IAsyncDisposable
{
    private readonly CancellationTokenSource _cts = new();
    private bool _disposed = false;
    private bool _cachedLayers = false;
    private Stack<BaseTransaction> _transactions = [];

    public bool CachedLayers { get => _cachedLayers; }

    public SagaContext()
    { }

    public SagaContext(bool cahcedLayers)
    {
        _cachedLayers = cahcedLayers;
    }

    public void WriteSaga(BaseTransaction transaction) => _transactions.Push(transaction);

    public ISagaLayer? GetCurrentSagaLayer(Type sagaLayerType)
    {
        if (!_cachedLayers)
            throw new MemberAccessException("ISagaLayer is only accessible with layer caching enabled.");

        return _transactions.SingleOrDefault(x => string.Equals(x.SourceTransactionType.FullName, sagaLayerType.FullName))?.SourceTransaction;
    }

    public void Commit()
    {
        foreach (var transaction in _transactions)
            transaction.Commit();
    }

    public async Task CommitAsync(CancellationToken cancellationToken = default)

    {
        foreach (var transaction in _transactions)
            await transaction.CommitAsync(cancellationToken);
    }

    public void Rollback()
    {
        foreach (var transaction in _transactions)
            transaction.Rollback();
    }

    public async Task RollbackAsync(CancellationToken cancellationToken = default)
    {
        foreach (var transaction in _transactions)
            await transaction.RollbackAsync(cancellationToken);
    }

    #region IDisposable, IAsyncDisposable

    public void Dispose()
    {
        if (_disposed) return;

        _cts.Cancel();
        _cts.Dispose();

        foreach (var transaction in _transactions)
            transaction.Dispose();

        GC.SuppressFinalize(this);
        _disposed = true;
    }

    public async ValueTask DisposeAsync()
    {
        if (_disposed) return;

        _cts.Cancel();

        foreach (var transaction in _transactions)
            await transaction.DisposeAsync();

        _cts.Dispose();

        GC.SuppressFinalize(this);
        _disposed = true;
    }

    ~SagaContext() => Dispose();

    #endregion IDisposable, IAsyncDisposable
}