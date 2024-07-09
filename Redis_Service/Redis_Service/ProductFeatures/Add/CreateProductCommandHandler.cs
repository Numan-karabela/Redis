using MediatR;
using Redis.Cache;
using Redis.Repositories;

namespace Redis.ProductFeatures
{
    public class ProductCommandHandler : IRequestHandler<CreateProductCommandReques, CreateProductCommandResponse>
    {   readonly IProductRepository productRepository; 
        readonly IRedisCacheService _cacheService;
        public ProductCommandHandler(IProductRepository productRepository, IRedisCacheService cacheService = null)
        {
            this.productRepository = productRepository; 
            _cacheService = cacheService;
        }

        public async Task<CreateProductCommandResponse> Handle(CreateProductCommandReques request, CancellationToken cancellationToken)
        {
              
             var product= CreateProductCommandReques.Map(request); 
             await productRepository.AddAsync(product,cancellationToken);
             await _cacheService.SetAsync(product,cancellationToken);

             
            return new()
            {
             Message="Başarılı"
            };
        }
    }
}
