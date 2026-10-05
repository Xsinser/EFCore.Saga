namespace Saga.Microsoft.EntityFrameworkCore
{
    public partial class CachedSagaContext : SagaContext
    {
        public CachedSagaContext(SagaContext sagaContext) : base(sagaContext)
        {
            if (!this.CachedLayers)
                throw new InvalidOperationException("CachedSagaContext supported only CachedLayers SagaContext");
        }

        public T? GetCurrentSagaLayer<T>() where T : class, ISagaLayer
        {
            var type = typeof(T);
            var result = Transactions.FirstOrDefault(x => string.Equals(x.SourceTransactionType.FullName, type.FullName))?.SourceTransaction;

            if (result != null)
                return result as T;
            else
                return null;
        }

        #region IDisposable, IAsyncDisposable

        public new void Dispose()
        {
            if (_disposed) return;

            Transactions = null;

            _disposed = true;
        }

        public new async ValueTask DisposeAsync()
        {
            if (_disposed) return;

            Transactions = null;

            _disposed = true;
        }

        ~CachedSagaContext() => Dispose();

        #endregion IDisposable, IAsyncDisposable
    }
}