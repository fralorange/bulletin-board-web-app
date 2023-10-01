using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace BulletinBoard.Hosts.Api
{
    /// <summary>
    /// Методы расширения для <see cref="SwaggerGenOptions"/>
    /// </summary>
    public static class SwaggerGenOptionsExtensions
    {
        /// <summary>
        /// Добавление документации сваггера.
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public static SwaggerGenOptions AddSwaggerDoc(this SwaggerGenOptions options)
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "BulletinBoard", Version = "v1" });

            return options;
        }

        /// <summary>
        /// Добавление XML-документации элементов путём рефлексии.
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public static SwaggerGenOptions AddXmlDoc(this SwaggerGenOptions options)
        {
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

            return options;
        }

        public static SwaggerGenOptions AddSecurity(this SwaggerGenOptions options)
        {
            #region SecurityDefenition
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
            #endregion
            #region SecurityRequirement
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
            #endregion
            return options;
        }
    }
}
