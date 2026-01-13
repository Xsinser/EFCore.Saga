using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using TestSaga.Db.Entity;
using System.Runtime.CompilerServices;
using System.Transactions;
using Saga.Microsoft.EntityFrameworkCore;

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