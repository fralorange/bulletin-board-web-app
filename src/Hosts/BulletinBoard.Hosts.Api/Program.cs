using AutoMapper;
using BulletinBoard.Application.AppServices.Contexts.Ad.Repositories;
using BulletinBoard.Application.AppServices.Contexts.Ad.Services;
using BulletinBoard.Infrastructure.ComponentRegistrar.Mappers.Ad;
using BulletinBoard.Infrastructure.ComponentRegistrar.Mappers.Attachment;
using BulletinBoard.Infrastructure.DataAccess.Contexts.Ad.Repositories;
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

            builder.Services.AddAutoMapper(typeof(AdMapper), typeof(AttachmentMapper));

            builder.Services.AddTransient<IAdService, AdService>();
            builder.Services.AddSingleton<IAdRepository, AdRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}