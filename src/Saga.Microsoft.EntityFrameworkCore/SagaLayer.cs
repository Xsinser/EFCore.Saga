using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Saga.Microsoft.EntityFrameworkCore;

public class SagaLayer : DbContext, ISagaLayer
{
    public SagaLayer(DbContextOptions options) : base(options)
    {
    }

    public void BeginSagaTransaction(SagaContext context)
    {
        if (context.CachedLayers)
        {
            var currentTransaction = Database.CurrentTransaction;
            if (currentTransaction == null)
                context.WriteSaga(new Transaction(this, Database.BeginTransaction()));
        }
        else
            context.WriteSaga(new Transaction(this, Database.CurrentTransaction ?? Database.BeginTransaction()));
    }

    public async Task BeginSagaTransactionAsync(SagaContext context, CancellationToken cancellationToken = default)
    {
        if (context.CachedLayers)
        {
            var currentTransaction = Database.CurrentTransaction;
            if (currentTransaction == null)
                context.WriteSaga(new Transaction(this, await Database.BeginTransactionAsync(cancellationToken)));
        }
        else
            context.WriteSaga(new Transaction(this, Database.CurrentTransaction ?? await Database.BeginTransactionAsync(cancellationToken)));
    }

    protected new DatabaseFacade Database { get => base.Database; }

    public override void Dispose()
    {
        base.Dispose();
    }
}