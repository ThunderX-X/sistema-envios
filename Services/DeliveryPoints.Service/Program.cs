using DeliveryPoints.Service.Connection;
using DeliveryPoints.Service.Interfaces;
using DeliveryPoints.Service.MapperProfiles;
using DeliveryPoints.Service.Models;
using DeliveryPoints.Service.Services;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DeliveryPoints.Service
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            ConfigureLogger(builder);
            ConfigureDatabaseConnection(builder);
            CofigureApiServices(builder);
            ConfigureCollections(builder);
            ConfigureMappers(builder);
            ConfigureCors(builder);

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.UseCors("All");

            app.Run();
        }

        private static void ConfigureDatabaseConnection(WebApplicationBuilder builder)
        {
            builder.Services.Configure<DeliveryPointConfig>(
                config => {
                    config.Database = Environment.GetEnvironmentVariable("MONGO_DATABASE") ?? "";
                    config.Server = Environment.GetEnvironmentVariable("MONGO_SERVER") ?? "";
                    config.Collection = Environment.GetEnvironmentVariable("MONGO_COLLECTION") ?? "";
                }
            );


            builder.Services.AddSingleton<MongoConfig>(
                    conf => conf.GetRequiredService<IOptions<DeliveryPointConfig>>().Value
                );
            builder.Services.AddSingleton<DeliveryPointConnection>();
        }

        private static void CofigureApiServices(WebApplicationBuilder builder)
        {
            builder.Services.AddTransient(typeof(DeliveryPointsService));
        }

        private static void ConfigureCollections(WebApplicationBuilder builder)
        {
            builder.Services.AddSingleton<IMongoCollection<DeliveryPoint>>(
                    config => config.GetRequiredService<DeliveryPointConnection>().Collection
                    );
        }

        private static void ConfigureMappers(WebApplicationBuilder builder)
        {
            builder.Services.AddAutoMapper(
                    typeof(DeliveryPointProfile)
                );
        }

        private static void ConfigureCors(WebApplicationBuilder builder)
        {
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: "All",
                                  policy =>
                                  {
                                      policy.AllowAnyOrigin();
                                      policy.AllowAnyHeader();
                                      policy.AllowAnyMethod();
                                  });
            });
        }
        private static void ConfigureLogger(WebApplicationBuilder builder)
        {
            string enviromentVariable = Environment.GetEnvironmentVariable("LOG_LEVEL") ?? "Debug";
            bool correctLevel = Enum.TryParse(enviromentVariable, out LogLevel logLevel);
            builder.Logging.ClearProviders();
            builder.Logging.AddAWSProvider();
            builder.Logging.AddConsole();
            builder.Logging.SetMinimumLevel(correctLevel ? logLevel : LogLevel.Debug);
        }
    }
}