using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace Saga;

public class SagaContext : IDisposable, IAsyncDisposable
{
    private readonly CancellationTokenSource _cts = new();
    private bool _disposed;
    private Stack<ITransaction> _transactions = [];

    public void WriteSaga(ITransaction transaction) => _transactions.Push(transaction);

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

