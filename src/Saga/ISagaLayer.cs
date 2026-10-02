namespace Saga
{
    public interface ISagaLayer : IDisposable
    {
        void BeginSagaTransaction(SagaContext context);

        Task BeginSagaTransactionAsync(SagaContext context, CancellationToken cancellationToken = default);
    }
}