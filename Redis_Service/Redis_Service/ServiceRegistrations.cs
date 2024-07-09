using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Redis.Context;
using Redis.Repositories; 

namespace Redis
{
    public static class ServiceRegistrations
    {
        public static void AddPersistanceService(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddDbContextPool<CacheNQueueDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("Sql"))); 
            service.AddStackExchangeRedisCache(Options => Options.Configuration = (configuration["Redis:RedisHost"]));
            service.AddScoped<IProductRepository, ProductRepository>();
        }
    }
}
