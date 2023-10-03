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
                options.AddSwaggerDoc();
                options.AddXmlDoc();
                options.AddSecurity();
            });

            builder.Services.AddAuth(builder);

            builder.Services.AddHttpContextAccessor();

            builder.Services.AddAutoMapper();

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