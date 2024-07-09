using MediatR;
using Redis.Cache;
using Redis.Entites;
using Redis.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redis.ProductFeatures
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest, ProductUpdateCommandResponse>
    {
        readonly IProductRepository _productRepository;
        readonly IRedisCacheService _cacheService;

        public UpdateProductCommandHandler(IProductRepository productRepository, IRedisCacheService cacheService = null)
        {
            _productRepository = productRepository;
            _cacheService = cacheService;
        }

        public async Task<ProductUpdateCommandResponse> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
        {
           Product product= UpdateProductCommandRequest.Map(request);

           await _productRepository.UpdateAsync(product, cancellationToken);
            await _cacheService.UpdatedAsync(product,cancellationToken);
            return new()
            {
                Message="başarılı"
            };
        }
    }
}
