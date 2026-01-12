
using Microsoft.EntityFrameworkCore;
using Saga.Db.Entity;

namespace Saga.Db.Context
{
    public class AlphaContext : DbContext
    {
        public DbSet<AlphaEntity> AlphaEntities { get; set; }

        public AlphaContext(DbContextOptions<AlphaContext> options) : base(options)
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }
    }
}