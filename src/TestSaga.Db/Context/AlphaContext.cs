using Microsoft.EntityFrameworkCore;
using Saga.Microsoft.EntityFrameworkCore;
using TestSaga.Db.Entity;

namespace TestSaga.Db.Context
{
    public class AlphaContext : SagaLayer
    {
        public DbSet<AlphaEntity> AlphaEntities { get; set; }

        public AlphaContext(DbContextOptions<AlphaContext> options) : base(options)
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }
    }
}