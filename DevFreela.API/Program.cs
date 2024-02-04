
using DevFreela.API.Models;

namespace DevFreela.API
{
    public class Program
    {

        public Program()
        {
            
        }

        public static IConfiguration Configuration { get; }

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.Configure<OpeningTimeOption>(builder.Configuration.GetSection("openingTime"))
                            .Configure<MaintenanceTimeOption>(builder.Configuration.GetSection("MaintenanceTime"));

            //.Services.AddSingleton<ExampleLifeTimeObject>(e => new ExampleLifeTimeObject { Example = "Initial Stage Singleton" });
            builder.Services.AddScoped<ExampleLifeTimeObject>(e => new ExampleLifeTimeObject { Example = "Initial Stage Scope" });

            // Add services to the container.
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

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
