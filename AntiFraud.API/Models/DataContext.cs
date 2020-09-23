using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace AntiFraud.API.Models
{
    public class DataContext : DbContext
    {
        public DbSet<Purchase> Purchases { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        public DataContext(DbContextOptions options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Purchase>().Property(x => x.Products).HasConversion(
                x => JsonConvert.SerializeObject(x),
                x => JsonConvert.DeserializeObject<List<Product>>(x)
                );

            // note: this is only needed
            // since SQLite does not support
            // decimal directly
            modelBuilder.Entity<Purchase>()
                .Property(e => e.Amount)
                .HasConversion<double>();

            modelBuilder.Entity<Purchase>().OwnsOne(p => p.Address);

            modelBuilder.Entity<Purchase>()
                .Property(b => b.Id)
                .ValueGeneratedOnAdd();
        }
    }
}
