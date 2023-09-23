using BulletinBoard.Infrastructure.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BulletinBoard.Infrastructure.DataAccess
{
    /// <inheritdoc cref="IDbContextOptionsConfigurator{TContext}"/>
    public class BaseDbContextOptionsConfigurator : IDbContextOptionsConfigurator<BaseDbContext>
    {
        private const string ConnectionStringName = "PostgresBoardDb";

        private readonly IConfiguration _configuration;

        /// <summary>
        /// Конструктор конфигурации контекста БД.
        /// </summary>
        /// <param name="configuration">Конфигурация.</param>
        public BaseDbContextOptionsConfigurator(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        /// <inheritdoc/>
        public void Configure(DbContextOptionsBuilder<BaseDbContext> options)
        {
            var connectionString = _configuration.GetConnectionString(ConnectionStringName);

            options
                .UseNpgsql(connectionString)
                .EnableDetailedErrors(true);
        }
    }
}
