using AutoMapper;
using Clients.Service.Connection;
using Clients.Service.Interfaces;
using Clients.Service.MapperProfiles;
using Clients.Service.Services;
using Microsoft.Extensions.Options;

namespace Clients.Service
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddAutoMapper(typeof(ClientProfile));

            builder.Logging.AddAWSProvider();
            ConfigureDatabaseConnection(builder);
            ConfigureLogger(builder);
            ConfigureCors(builder);


            builder.Services.AddTransient<ClientService>();

            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.UseCors("All");

            app.Run();
        }

        private static void ConfigureDatabaseConnection(WebApplicationBuilder builder)
        {
            builder.Services.Configure<ClientConfig>(
                config => {
                    config.Database = Environment.GetEnvironmentVariable("MONGO_DATABASE") ?? "";
                    config.Server = Environment.GetEnvironmentVariable("MONGO_SERVER") ?? "";
                    config.Collection = Environment.GetEnvironmentVariable("MONGO_COLLECTION") ?? "";
                }
                );
            builder.Services.AddSingleton<MongoConfig>(
                    conf => conf.GetRequiredService<IOptions<ClientConfig>>().Value
                );
            builder.Services.AddSingleton<ClientConnection>();
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