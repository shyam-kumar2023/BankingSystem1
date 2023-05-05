using BankingSystem.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.Domain
{
    public class PGDbContext: DbContext
    {
        public PGDbContext(DbContextOptions<PGDbContext> options) : base(options)
        {
        }

        public DbSet<AccountEntity> Accounts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();
            return base.SaveChanges();
        }
    }
}
