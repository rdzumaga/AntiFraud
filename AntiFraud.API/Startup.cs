using AntiFraud.API.FraudCheckers;
using AntiFraud.API.Models;
using Hangfire;
using Hangfire.MemoryStorage;
using Hangfire.Storage.SQLite;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Data.Common;

namespace AntiFraud.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        private DbConnection CreateInMemoryDatabase()
        {
            var connection = new SqliteConnection("Data Source=InMemoryDb;Mode=Memory;Cache=Shared");

            connection.Open();

            return connection;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
#if USE_IN_MEMORY_SQLITE
            services.AddDbContext<DataContext>(options => options.UseSqlite(CreateInMemoryDatabase()));
#else
            services.AddDbContext<DataContext>(options => options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));
#endif

            services.AddHangfire(configuration => {
                configuration.SetDataCompatibilityLevel(CompatibilityLevel.Version_170);
                configuration.UseSimpleAssemblyNameTypeSerializer();
                configuration.UseRecommendedSerializerSettings();
#if USE_IN_MEMORY_SQLITE
                configuration.UseMemoryStorage();
#else
                configuration.UseSQLiteStorage(Configuration.GetConnectionString("Hangfire"));
#endif
            });

            services.AddTransient<IFraudChecker, NigerianPrinceFraudChecker>();
            services.AddTransient<IFraudChecker, UnusuallyHighAmountFraudChecker>();

            services.AddHangfireServer();

            services.AddControllersWithViews();
            // In production, the Angular files will be served from this directory

            services.AddSwaggerDocument();

            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseOpenApi();
            app.UseSwaggerUi3();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
                endpoints.MapHangfireDashboard();
            });

            RecurringJob.AddOrUpdate(
                () => System.Console.WriteLine("Recurring!"),
                Cron.Minutely);

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
