using DDD.App.Api.Applicationses.IntegrationEvents;
using DDD.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DDD.App.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDomainContext(this IServiceCollection services, Action<DbContextOptionsBuilder> optionsAction)
        {
            return services.AddDbContext<DomainContext>(optionsAction);
        }
        public static IServiceCollection AddMysqlDomainContext(this IServiceCollection services, IConfiguration configuration)
        {
            // Replace with your connection string.
            var connectionString = configuration["Database:ConnectionString"];
            var serverVersion = new MySqlServerVersion(new Version(5, 7, 36));

            return services.AddDomainContext(builder =>
            {
                Console.WriteLine($"add mysql domain context {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}");
                builder.UseMySql(connectionString, serverVersion);
                    //// The following three options help with debugging, but should
                    //// be changed or removed for production.
                    //.LogTo(Console.WriteLine, LogLevel.Debug)
                    //.EnableSensitiveDataLogging()
                    //.EnableDetailedErrors();
            });
        }

        public static IServiceCollection AddEventBus(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<ISubscriberService, SubscriberService>();
            services.AddCap(options =>
            {
                var connectionString = configuration["Database:ConnectionString"];
                //如果数据库上下文(DomainContext)中注入了ICapPublisher，则只能用UseMySql();不能用UseEntityFramework，否则项目启动会死循环
                //options.UseEntityFramework<DomainContext>();
                options.UseMySql(connectionString);
                options.UseRabbitMQ(options =>
                {
                    configuration.GetSection("RabbitMQ").Bind(options);
                });
            });

            return services;
        }
    }
}
