using AntiFraud.API.Dto;
using AntiFraud.API.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
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

            using (var scope = host.Services.CreateScope())
            {                
                var context = (DataContext)scope.ServiceProvider.GetService(typeof(DataContext));
                Seed(context);
            }            
            
            host.Run();
        }

        private static void Seed(DataContext dataContext)
        {
            try
            {
                dataContext.Database.EnsureCreated();

                var filename = "seed.json";
                if (!File.Exists(filename)) return;
                if (dataContext.Purchases.Count() > 0) return;

                var json = File.ReadAllText(filename);
                var purchases = JsonConvert.DeserializeObject<List<PurchaseDto>>(json);
                dataContext.Purchases.AddRange(purchases.Select(x => x.ToDomainObject()));
                dataContext.SaveChanges();
            }
            catch (System.Exception ex)
            {
                // todo log error
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
