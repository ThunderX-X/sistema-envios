using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Shipments.Service.Connection;
using Shipments.Service.Interfaces;
using Shipments.Service.MapperProfiles;
using Shipments.Service.Models;
using Shipments.Service.Proxies;
using Shipments.Service.Services;

namespace Shipments.Service
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            ConfigureSwaggerService(builder);
            ConfigureApiServices(builder);
            ConfigureConnections(builder);
            ConfigureMapper(builder);
            ConfigureLogger(builder);
            ConfigureCors(builder);

            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.UseCors("All");

            app.Run();
        }

        private static void ConfigureApiServices(WebApplicationBuilder builder)
        {
            ConfigureClientssProxy(builder);
            ConfigureDeliveryPointsProxy(builder);
            builder.Services.AddTransient(typeof(ShippingService));
            builder.Services.AddTransient(typeof(HttpClient));
        }

        private static void ConfigureDeliveryPointsProxy(WebApplicationBuilder builder)
        {
            builder.Services.Configure<DeliveryPointsUrl>(
                config =>
                    config.Url = Environment.GetEnvironmentVariable("DELIVERY_POINTS_SERVICE") ?? ""
                );
            builder.Services.AddTransient(typeof(DeliveryPointsProxy));
        }

        private static void ConfigureClientssProxy(WebApplicationBuilder builder) {
            builder.Services.Configure<ClientUrl>(
                    config => config.Url = Environment.GetEnvironmentVariable("CLIENTS_SERVICE") ?? ""
                ) ;
            builder.Services.AddTransient(typeof(ClientsProxy));
        }

        private static void ConfigureConnections(WebApplicationBuilder builder)
        {
            builder.Services.Configure<ShippingConfig>(
                config => {
                    config.Database = Environment.GetEnvironmentVariable("MONGO_DATABASE") ?? "";
                    config.Server = Environment.GetEnvironmentVariable("MONGO_SERVER") ?? "";
                    config.Collection = Environment.GetEnvironmentVariable("MONGO_COLLECTION") ?? "";
                }
                );
            builder.Services.AddSingleton<MongoConfig>(
                    conf => conf.GetRequiredService<IOptions<ShippingConfig>>().Value
                );
            builder.Services.AddSingleton<ShippingConnection>();
            builder.Services.AddTransient<IMongoCollection<Shipping>>(
                    conf => conf.GetRequiredService<ShippingConnection>().Collection
                );
        }

        private static void ConfigureMapper(WebApplicationBuilder builder)
        {
            builder.Services.AddAutoMapper(
                typeof(ShippingProfile)
                );
        }

        private static void ConfigureLogger(WebApplicationBuilder builder)
        {
            string enviromentVariable = Environment.GetEnvironmentVariable("LOG_LEVEL") ?? "Debug";
            bool correctLevel = Enum.TryParse(enviromentVariable, out LogLevel logLevel);
            builder.Logging.ClearProviders();
            builder.Logging.AddAWSProvider();
            builder.Logging.AddConsole();
            builder.Logging.SetMinimumLevel(correctLevel? logLevel : LogLevel.Debug);
        }

        private static void ConfigureSwaggerService(WebApplicationBuilder builder)
        {
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
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
    }
}