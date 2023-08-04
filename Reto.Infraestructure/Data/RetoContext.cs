using Microsoft.EntityFrameworkCore;
using Reto.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reto.Infraestructure.Data
{
    public class RetoContext : DbContext
    {
        public RetoContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(RetoContext).Assembly);
        }
    }
}
