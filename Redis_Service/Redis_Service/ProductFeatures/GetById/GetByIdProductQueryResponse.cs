using Redis.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redis.ProductFeatures
{
    public class GetByIdProductQueryResponse
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; } 
        public static GetByIdProductQueryResponse Map(Product product)
        {
            
            return new GetByIdProductQueryResponse()
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock,

            };


        }

    }
}
