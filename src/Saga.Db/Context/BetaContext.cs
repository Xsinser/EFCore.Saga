
using Microsoft.EntityFrameworkCore;
using Saga.Db.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saga.Db.Context
{
    public class BetaContext : DbContext
    {
        public DbSet<BetaEntity> BetaEntities { get; set; }

        public BetaContext(DbContextOptions<AlphaContext> options) : base(options)
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }
    }
}
