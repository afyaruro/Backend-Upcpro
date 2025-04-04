using Domain.Port.User;
using Infrastructure.Config;
using Infrastructure.Context.MongoDB;
using Infrastructure.Extensions.Start;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Infrastructure.Extensions.Persistence
{
    public static class MongoDBExtensions
    {
        public static IServiceCollection AddMongoDB(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ConfigMongoDB>(configuration.GetSection("ConnectionStrings"));
            services.AddSingleton<IConfigMongoDB>(d => d.GetRequiredService<IOptions<ConfigMongoDB>>().Value);
            services.AddSingleton<MongoDBContext>();

            return services;
        }

        public static async Task InitializeDatabaseAsync(IServiceProvider serviceProvider)
        {
            var userRepository = serviceProvider.GetRequiredService<IUserRepository>();
            var startMongoDB = new StartMongoDB(userRepository);
            await startMongoDB.CreateAdminStart();
        }
    }
}