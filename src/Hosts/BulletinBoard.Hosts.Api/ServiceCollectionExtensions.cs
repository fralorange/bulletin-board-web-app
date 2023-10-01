using BulletinBoard.Application.AppServices.Authentication;
using BulletinBoard.Application.AppServices.Authentication.Constants;
using BulletinBoard.Application.AppServices.Authentication.Handlers;
using BulletinBoard.Application.AppServices.Contexts.Ad.Repositories;
using BulletinBoard.Application.AppServices.Contexts.Ad.Services;
using BulletinBoard.Infrastructure.ComponentRegistrar.Mappers.Ad;
using BulletinBoard.Infrastructure.ComponentRegistrar.Mappers.Attachment;
using BulletinBoard.Infrastructure.DataAccess;
using BulletinBoard.Infrastructure.DataAccess.Contexts.Ad.Repositories;
using BulletinBoard.Infrastructure.DataAccess.Interfaces;
using BulletinBoard.Infrastructure.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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
                configurator.Configure((DbContextOptionsBuilder<BaseDbContext>)options);
            });
            services.AddScoped<DbContext, BaseDbContext>();

            return services;
        }

        /// <summary>
        /// Добавляет Автомаппер в DI.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AdMapper), typeof(AttachmentMapper));

            return services;
        }

        /// <summary>
        /// Добавляет аутентификацию и авторизацию.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IServiceCollection AddAuth(this IServiceCollection services, WebApplicationBuilder builder)
        {
            #region Authentication
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
                    };
                })
                .AddScheme<JwtSchemeOptions, JwtSchemeHandler>(AuthSchemas.Custom, options => { });
            #endregion
            #region Authorization
            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("CustomPolicy", policy =>
            //    {
            //        policy.RequireRole("Administrator");
            //        policy.RequireClaim("User", "User");
            //    });
            //});
            services.AddAuthorization();

            #endregion
            return services;
        }
    }
}
