
using MediatR;
using Redis.Entites;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redis.ProductFeatures
{
    public class CreateProductCommandReques:IRequest<CreateProductCommandResponse>
    { 
            public string Name { get; set; } 
            public string Description { get; set; }    
            public decimal Price { get; set; }  
            public int Stock { get; set; }  
        public static Product Map(CreateProductCommandReques command)
        {
            return new Product()
            {   Id = Guid.NewGuid(),
                Name = command.Name,
                Price = command.Price,
                Stock = command.Stock,
                Description = command.Description,

            };

        }
         
    }
}
