
using Microsoft.EntityFrameworkCore;
using Redis.Entites;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redis.Context
{
    public class CacheNQueueDbContext: DbContext 
    {
         
        public CacheNQueueDbContext(DbContextOptions<CacheNQueueDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; } // Ürünler tablosu 

    }
}
