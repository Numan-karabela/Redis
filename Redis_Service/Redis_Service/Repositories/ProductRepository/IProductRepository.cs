using Redis.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redis.Repositories
{
      public interface IProductRepository 
    {
        Task<Product> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<List<Product>> GetAllAsync(CancellationToken cancellationToken);

        Task AddAsync(Product entity, CancellationToken cancellationToken);

        Task UpdateAsync(Product entity, CancellationToken cancellationToken);

        Task RemoveAsync(Product entity, CancellationToken cancellationToken);
    }
}
