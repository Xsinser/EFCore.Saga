using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saga.Microsoft.EntityFrameworkCore;

public class SagaLayer : DbContext
{
    public SagaLayer(DbContextOptions options) : base(options)
    {
    }

    public void BeginSagaTransaction(SagaContext context)
    {
        context.WriteSaga(new Transaction(Database.BeginTransaction()));
    }

    public async Task BeginSagaTransactionAsync(SagaContext context, CancellationToken cancellationToken = default)
    {
        context.WriteSaga(new Transaction(await Database.BeginTransactionAsync(cancellationToken)));
    }

    protected new DatabaseFacade Database { get => base.Database; }
}

