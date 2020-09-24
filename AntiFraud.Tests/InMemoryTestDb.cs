using AntiFraud.API.Models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace AntiFraud.Tests
{
    public class InMemoryTestDb : IDisposable
    {
        protected InMemoryTestDb()
        {
            ContextOptions = new DbContextOptionsBuilder<DataContext>()
                    .UseSqlite(CreateInMemoryDatabase())
                    .Options;

            _connection = RelationalOptionsExtension.Extract(ContextOptions).Connection;

            Seed();
        }

        protected DbContextOptions<DataContext> ContextOptions { get; }

       

        private readonly DbConnection _connection;
       

        private static DbConnection CreateInMemoryDatabase()
        {
            var connection = new SqliteConnection("Filename=:memory:");

            connection.Open();

            return connection;
        }

        public void Dispose() => _connection.Dispose();

        private void Seed()
        {
            using (var context = new DataContext(ContextOptions))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                context.Purchases.Add(new Purchase("rafal@1dzumaga.pl", 100, "PLN", new Address("s", "z", "c", "pl"), new List<Product>() { new Product("1", 1) }));
                context.Purchases.Add(new Purchase("rafal@2dzumaga.pl", 200, "PLN", new Address("s", "z", "c", "pl"), new List<Product>() { new Product("1", 1) }));
                context.Purchases.Add(new Purchase("rafal@3dzumaga.pl", 300, "PLN", new Address("s", "z", "c", "pl"), new List<Product>() { new Product("1", 1) }));


                context.Purchases.Add(new Purchase("nigerian.king@wp.pl", 300, "PLN", new Address("s", "z", "c", "pl"), new List<Product>() { new Product("1", 1) }));

                var validPurchase = new Purchase("nigerian.prince@wp.pl", 300, "PLN", new Address("s", "z", "c", "nigeria"), new List<Product>() { new Product("1", 1) });
                validPurchase.SetValid();
                context.Purchases.Add(validPurchase);

                context.SaveChanges();
            }
        }
    }
}
