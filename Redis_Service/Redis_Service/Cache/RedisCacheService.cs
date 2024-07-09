

using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Redis.Entites;
using Redis.Repositories;
using System.Text.Json;

namespace Redis.Cache
{
    public class RedisCacheService : IRedisCacheService
    {
        readonly IDistributedCache cache;
        readonly IConfiguration configuration;
        readonly IProductRepository productRepository;
        
        public RedisCacheService(IDistributedCache cache, IConfiguration configuration, IProductRepository productRepository)
        {
            this.cache = cache;
            this.configuration = configuration;
            this.productRepository = productRepository; 
        }

       
        public async Task<List<Product>> GetAllAsync(CancellationToken cancellationToken)
        {
            var productString = await cache.GetStringAsync(configuration["Redis:GettAllRedis"], cancellationToken);
            if (string.IsNullOrEmpty(productString))
            { 
                var products = await productRepository.GetAllAsync(cancellationToken);
                await SetAllAsync(products, cancellationToken);

                if (products == null)
                {
                    return null;
                }
            }
            return JsonSerializer.Deserialize<List<Product>>(productString);
        }

        public async Task<Product> GetByıdAsync(Guid key, CancellationToken cancellationToken)
        {
            var productString = await cache.GetStringAsync(Convert.ToString(key), cancellationToken);
            if (string.IsNullOrEmpty(productString))
            { 
                var productCache =await productRepository.GetByIdAsync(key,cancellationToken); 
                var map = Convert.ToString(productCache);
                if (string.IsNullOrEmpty(map))
                {
                    return null;
                }
                await cache.SetStringAsync($"{JsonSerializer.Serialize(productCache.Id)}", JsonSerializer.Serialize(productCache),cancellationToken);


                return productCache;

            }
            return JsonSerializer.Deserialize<Product>(productString);
        }


        public async Task SetAsync(Product product, CancellationToken cancellationToken)
        {
            var Control= await GetByıdAsync(product.Id,cancellationToken);
            if (Control != null)
            {
                Console.WriteLine("veri sistemde kayıtlı");
            }
            await cache.SetStringAsync($"{JsonSerializer.Serialize(product.Id)}", JsonSerializer.Serialize(product));
            var products = JsonSerializer.Deserialize<List<Product>>(await cache.GetStringAsync(configuration["Redis:GettAllRedis"]));
            products = products.ToList();
            products.Add(product);
            await cache.RemoveAsync(configuration["Redis:GettAllRedis"]);
            await SetAllAsync(products, cancellationToken);
        }

        public async Task SetAllAsync(List<Product> product, CancellationToken cancellationToken) 
        {
            await cache.RemoveAsync(configuration["Redis:GettAllRedis"]);
            await cache.SetStringAsync(configuration["Redis:GettAllRedis"], JsonSerializer.Serialize(product)); 
        }

        public async Task DeleteAsync(Guid key, CancellationToken cancellationToken)
        {
            var products = JsonSerializer.Deserialize<List<Product>>(await cache.GetStringAsync(configuration["Redis:GettAllRedis"]));
            await cache.RemoveAsync($"{Convert.ToString(key)}");
            products = products.Where(x => x.Id != key).ToList();
            await SetAllAsync(products, cancellationToken);

        }
        public async Task DeleteeAsync(Guid key, CancellationToken cancellationToken)
        {
            var products = JsonSerializer.Deserialize<List<Product>>(await cache.GetStringAsync(configuration["Redis:GettAllRedis"]));
            await cache.RemoveAsync($"{Convert.ToString(key)}");
            products = products.Where(x => x.Id != key).ToList();
            await SetAllAsync(products, cancellationToken);

        }

        public async Task  UpdatedAsync(Product product, CancellationToken cancellationToken)
        {
            var products = JsonSerializer.Deserialize<List<Product>>(await cache.GetStringAsync(configuration["Redis:GettAllRedis"]));
            var productkey = Convert.ToString(product.Id);
            await cache.RemoveAsync(productkey);
            products = products.Where(x => x.Id != product.Id).ToList();
            Product productsUpdate = new() { Id = product.Id, Name = product.Name, Price = product.Price, Stock = product.Stock, Description = product.Description};
            products.Add(productsUpdate);
            await SetAllAsync(products, cancellationToken);

        }
    }
}