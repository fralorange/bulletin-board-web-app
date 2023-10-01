using BulletinBoard.Application.AppServices.Contexts.Ad.Repositories;
using BulletinBoard.Application.AppServices.Contexts.Ad.Services;
using BulletinBoard.Domain.Ad;
using BulletinBoard.Infrastructure.DataAccess;
using BulletinBoard.Infrastructure.DataAccess.Contexts.Ad.Repositories;
using BulletinBoard.Infrastructure.DataAccess.Interfaces;
using BulletinBoard.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BulletinBoard.Hosts.Api
{
    /// <summary>
    /// Методы расширения для добавления сервисов в DI.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Добавляет сервисы в DI.
        /// </summary>
        /// <param name="services">Сервисы приложения.</param>
        /// <returns></returns>
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<IAdService, AdService>();

            return services;
        }

        /// <summary>
        /// Добавляет репозитории в DI.
        /// </summary>
        /// <param name="services">Сервисы приложения.</param>
        /// <returns></returns>
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IAdRepository, AdRepository>();

            return services;
        }

        /// <summary>
        /// Добавляет контекст БД в DI.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddDbContextConfiguration(this IServiceCollection services)
        {
            services.AddScoped<IDbInitializer, EfDbInitializer>();
            
            services.AddSingleton<IDbContextOptionsConfigurator<BaseDbContext>, BaseDbContextOptionsConfigurator>();
            services.AddDbContext<BaseDbContext>((serviceProvider, options) =>
            {
                var configurator = serviceProvider.GetRequiredService<IDbContextOptionsConfigurator<BaseDbContext>>();
                configurator.Configure((DbContextOptionsBuilder<BaseDbContext>) options);
            });
            services.AddScoped<DbContext, BaseDbContext>();

            return services;
        }
    }
}
