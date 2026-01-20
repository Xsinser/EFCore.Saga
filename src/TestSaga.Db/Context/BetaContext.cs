using Microsoft.EntityFrameworkCore;
using Saga.Microsoft.EntityFrameworkCore;
using TestSaga.Db.Entity;

namespace TestSaga.Db.Context
{
    public class BetaContext : SagaLayer
    {
        public DbSet<BetaEntity> BetaEntities { get; set; }

        public BetaContext(DbContextOptions<BetaContext> options) : base(options)
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }
    }
}