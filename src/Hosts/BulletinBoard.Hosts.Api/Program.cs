using BulletinBoard.Hosts.Api.Authentication;
using BulletinBoard.Infrastructure.ComponentRegistrar.Mappers.Ad;
using BulletinBoard.Infrastructure.ComponentRegistrar.Mappers.Attachment;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

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
            // TO-DO: Create SwaggerGenOptionsExtension?
            builder.Services.AddSwaggerGen(options =>
            {
                // Swagger Doc
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "BulletinBoard", Version = "v1" });
                // XML-Documentation using System.Reflection;
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
                //
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme.
                                    Enter 'Bearer' [space] and then your token in the text input below.
                                    Example: 'Bearer secretKey'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = JwtBearerDefaults.AuthenticationScheme,
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer",
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });
            });

            //TO-DO: Add to ServiceCollectionExtensions
            //Auth
            #region Authentication
            builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
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
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                    };
                })
                .AddScheme<JwtSchemeOptions, JwtSchemeHandler>("CustomScheme", options => { });
            #endregion
            #region Authorization
            //builder.Services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("CustomPolicy", policy =>
            //    {
            //        policy.RequireRole("Administrator");
            //        policy.RequireClaim("User", "User");
            //    });
            //});
            builder.Services.AddAuthorization();

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