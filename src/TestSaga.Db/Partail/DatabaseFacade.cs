namespace Microsoft.EntityFrameworkCore.Infrastructure;

public class DatabaseSageFacade : DatabaseFacade
{
    public DatabaseSageFacade(DbContext context) : base(context)
    {
    }
}