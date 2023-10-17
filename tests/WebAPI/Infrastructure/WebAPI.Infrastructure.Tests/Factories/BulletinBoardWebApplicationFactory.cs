using BulletinBoard.Hosts.Api;
using BulletinBoard.Infrastructure.DataAccess;
using BulletinBoard.Infrastructure.DataAccess.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebAPI.Infrastructure.Tests.Auth;

namespace WebAPI.Infrastructure.Tests.Factories
{
    public class BulletinBoardWebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor =
                    services.SingleOrDefault(d => d.ServiceType == typeof(IDbContextOptionsConfigurator<BaseDbContext>));

                services.Remove(descriptor!);

                services.AddSingleton<IDbContextOptionsConfigurator<BaseDbContext>, TestBaseDbContextConfiguration>();

                var sp = services.BuildServiceProvider();
                using var scope = sp.CreateScope();
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<BaseDbContext>();

                services.AddAuthentication("test")
                    .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>("test", options => { });

                db.Database.EnsureCreated();
                DataSeedHelper.InitializeDbForTests(db);
            });
        }

        /// <summary>
        /// Создать контекст тестовой БД.
        /// </summary>
        /// <returns></returns>
        public BaseDbContext CreateDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<BaseDbContext>();
            optionsBuilder.UseInMemoryDatabase(TestBaseDbContextConfiguration.InMemoryDatabaseName);
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.UseLazyLoadingProxies();
            var dbContext = new BaseDbContext(optionsBuilder.Options);
            return dbContext;
        }
    }
}
