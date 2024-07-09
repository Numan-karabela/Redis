using Microsoft.EntityFrameworkCore;
using Redis.Context;
using Redis.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redis.Repositories
{
    public class ProductRepository : IProductRepository
    {
        protected readonly CacheNQueueDbContext _context;

        public ProductRepository(CacheNQueueDbContext context)
        {
            _context = context;
        }

        public async Task<Product> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Set<Product>().FindAsync(id, cancellationToken);
        }

        public async Task<List<Product>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Set<Product>().AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task AddAsync(Product entity, CancellationToken cancellationToken)
        {
            await _context.Set<Product>().AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync();
        }

        public async Task AddRangeAsync(List<Product> entities, CancellationToken cancellationToken)
        {

            await _context.Set<Product>().AddRangeAsync(entities, cancellationToken);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product entity, CancellationToken cancellationToken)
        {
            _context.Set<Product>().Update(entity);
            _context.SaveChanges();
        }

        public async Task RemoveAsync(Product entity, CancellationToken cancellationToken)
        {
            _context.Set<Product>().Remove(entity);
            _context.SaveChanges();
        }
    }
}
