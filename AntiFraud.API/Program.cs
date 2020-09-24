using AntiFraud.API.Dto;
using AntiFraud.API.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AntiFraud.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            var logger = host.Services.GetRequiredService<ILogger<Program>>();

            try
            {
                using (var scope = host.Services.CreateScope())
                {
                    var context = (DataContext)scope.ServiceProvider.GetService(typeof(DataContext));
                    Seed(context);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error while seeding database");
            }
            
            host.Run();
        }

        private static void Seed(DataContext dataContext)
        {
            dataContext.Database.EnsureCreated();

            var filename = "seed.json";
            if (!File.Exists(filename)) return;
            if (dataContext.Purchases.Count() > 0) return;

            var json = File.ReadAllText(filename);
            var purchases = JsonConvert.DeserializeObject<List<PurchaseDto>>(json);
            var domainObjects = purchases.Select(x => x.ToDomainObject()).ToList();
            foreach (var domainObject in domainObjects)
            {
                domainObject.SetValid();
            }
            dataContext.Purchases.AddRange(domainObjects);
            dataContext.SaveChanges();            
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
