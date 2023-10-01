using BulletinBoard.Hosts.Api.Authentication;
using BulletinBoard.Infrastructure.ComponentRegistrar.Mappers.Ad;
using BulletinBoard.Infrastructure.ComponentRegistrar.Mappers.Attachment;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Reflection;

namespace BulletinBoard.Hosts.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                // using System.Reflection;
                var hostsXmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, hostsXmlFilename));

                var referencedAssemblies = Assembly.GetExecutingAssembly().GetReferencedAssemblies();
                foreach (var referencedAssembly in referencedAssemblies)
                {
                    if (!referencedAssembly.Name!.Contains("BulletinBoard"))
                        continue;
                    var xmlFilename = $"{referencedAssembly.Name}.xml";
                    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
                }
            });

            //TO-DO: Add to ServiceCollectionExtensions
            //Auth
            #region Authentication
            builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
                    options.SlidingExpiration = true;
                    options.Events.OnSignedIn = context =>
                    {
                        return Task.CompletedTask;
                    };
                })
                .AddScheme<AuthSchemeOptions, AuthSchemeHandler>("CustomScheme", options => { });
            #endregion
            #region Authorization
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("CustomPolicy", policy =>
                {
                    policy.RequireRole("Administrator");
                    policy.RequireClaim("User", "User");
                });
            });

            #endregion

            //AutoMapper
            builder.Services.AddAutoMapper(typeof(AdMapper), typeof(AttachmentMapper));
            //

            builder.Services.AddServices();
            builder.Services.AddRepositories();

            builder.Services.AddDbContextConfiguration();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}