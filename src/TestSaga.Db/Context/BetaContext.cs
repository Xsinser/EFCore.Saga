
using Microsoft.EntityFrameworkCore;
using TestSaga.Db.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestSaga.Db.Context
{
    public class BetaContext : DbContext
    {
        public DbSet<BetaEntity> BetaEntities { get; set; }

        public BetaContext(DbContextOptions<BetaContext> options) : base(options)
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }
    }
}
