using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saga.Microsoft.EntityFrameworkCore;

public class Transaction : ITransaction
{
    private readonly CancellationTokenSource _cts = new();
    private bool _disposed;
    private IDbContextTransaction _transaction;

    public Transaction(IDbContextTransaction transaction)
    {
        _transaction = transaction;
    }

    public void Commit() => _transaction.Commit();

    public async Task CommitAsync(CancellationToken cancellationToken = default) => await _transaction.CommitAsync(cancellationToken);

    public void Rollback() => _transaction.Rollback();

    public async Task RollbackAsync(CancellationToken cancellationToken = default) => await _transaction.RollbackAsync(cancellationToken);

    #region IDisposable, IAsyncDisposable

    public void Dispose()
    {
        if (_disposed) return;

        _cts.Cancel();
        _cts.Dispose();

        _transaction.Dispose();

        GC.SuppressFinalize(this);
        _disposed = true;
    }

    public async ValueTask DisposeAsync()
    {
        if (_disposed) return;

        _cts.Cancel();

        await _transaction.DisposeAsync();

        _cts.Dispose();

        GC.SuppressFinalize(this);
        _disposed = true;
    }

    ~Transaction() => Dispose();

    #endregion IDisposable, IAsyncDisposable
}

