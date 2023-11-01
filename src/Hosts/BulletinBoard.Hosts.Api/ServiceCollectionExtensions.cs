using AutoMapper;
using BulletinBoard.Application.AppServices.Authentication;
using BulletinBoard.Application.AppServices.Authentication.Constants;
using BulletinBoard.Application.AppServices.Authentication.Handlers;
using BulletinBoard.Application.AppServices.Authentication.Services;
using BulletinBoard.Application.AppServices.Contexts.Ad.Repositories;
using BulletinBoard.Application.AppServices.Contexts.Ad.Services;
using BulletinBoard.Application.AppServices.Contexts.Attachment.Repositories;
using BulletinBoard.Application.AppServices.Contexts.Attachment.Services;
using BulletinBoard.Application.AppServices.Contexts.Category.Repositories;
using BulletinBoard.Application.AppServices.Contexts.Category.Services;
using BulletinBoard.Application.AppServices.Contexts.Comment.Repositories;
using BulletinBoard.Application.AppServices.Contexts.Comment.Services;
using BulletinBoard.Application.AppServices.Contexts.User.Repositories;
using BulletinBoard.Application.AppServices.Contexts.User.Services;
using BulletinBoard.Infrastructure.ComponentRegistrar.Mappers.Ad;
using BulletinBoard.Infrastructure.ComponentRegistrar.Mappers.Attachment;
using BulletinBoard.Infrastructure.ComponentRegistrar.Mappers.Category;
using BulletinBoard.Infrastructure.ComponentRegistrar.Mappers.Comment;
using BulletinBoard.Infrastructure.ComponentRegistrar.Mappers.User;
using BulletinBoard.Infrastructure.DataAccess;
using BulletinBoard.Infrastructure.DataAccess.Contexts.Ad.Repositories;
using BulletinBoard.Infrastructure.DataAccess.Contexts.Attachment.Repositories;
using BulletinBoard.Infrastructure.DataAccess.Contexts.Category.Repositories;
using BulletinBoard.Infrastructure.DataAccess.Contexts.Comment.Repositories;
using BulletinBoard.Infrastructure.DataAccess.Contexts.User.Repositories;
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
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IAttachmentService, AttachmentService>();
            services.AddTransient<IEntityAuthorizationService, EntityAuthorizationService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<ICommentService, CommentService>();

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
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAttachmentRepository, AttachmentRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();

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
        public static IServiceCollection AddMapper(this IServiceCollection services)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AdMapper>();
                cfg.AddProfile<AttachmentMapper>();
                cfg.AddProfile<UserMapper>();
                cfg.AddProfile<CategoryMapper>();
                cfg.AddProfile<CommentMapper>();
            });

            config.AssertConfigurationIsValid();

            IMapper mapper = config.CreateMapper();
            services.AddSingleton(mapper);

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

        /// <summary>
        /// Добавляет кэш хранящийся в памяти веб-сервера.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddMemoryCaching(this IServiceCollection services)
        {
            services.AddMemoryCache(options =>
            {
                options.CompactionPercentage = 0.8;
                options.ExpirationScanFrequency = TimeSpan.FromMinutes(60); 
            });

            return services;
        }
    }
}
