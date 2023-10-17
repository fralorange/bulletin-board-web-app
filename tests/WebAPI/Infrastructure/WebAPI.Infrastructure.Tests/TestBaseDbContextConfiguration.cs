using BulletinBoard.Infrastructure.DataAccess;
using BulletinBoard.Infrastructure.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace WebAPI.Infrastructure.Tests
{
    public class TestBaseDbContextConfiguration : IDbContextOptionsConfigurator<BaseDbContext>
    {
        public const string InMemoryDatabaseName = "BaseDb";

        private readonly ILoggerFactory _loggerFactory;

        public TestBaseDbContextConfiguration(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
        }

        public void Configure(DbContextOptionsBuilder<BaseDbContext> options)
        {
            options.UseInMemoryDatabase(InMemoryDatabaseName);
            options.UseLoggerFactory(_loggerFactory);
            options.EnableSensitiveDataLogging();
            options.UseLazyLoadingProxies();
        }
    }
}
