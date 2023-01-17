using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShekelGroup_Amit_Dahari.Models;

namespace ShekelGroup_Amit_Dahari
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Factory> Factories { get; set; }
        public DbSet<FactoriesToCustomer> FactoriesToCustomers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FactoriesToCustomer>()
            .HasKey(ftc => new { ftc.GroupCode, ftc.FactoryCode, ftc.CustomerId });

            modelBuilder.Entity<Customer>()
            .HasKey(c => c.CustomerId);

            modelBuilder.Entity<Factory>()
            .HasKey(f => f.FactoryCode);

            modelBuilder.Entity<Group>()
            .HasKey(g => g.GroupCode);
        }
    }

}
