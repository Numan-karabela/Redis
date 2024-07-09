 
using MediatR;
using Redis.Cache;
using Redis.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redis.ProductFeatures
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommandRequest, DeleteProductCommandResponse>
    {
        private readonly IProductRepository _productRepository;
        readonly IRedisCacheService _cacheService;
        public DeleteProductCommandHandler(IProductRepository productRepository, IRedisCacheService cacheService = null)
        {
            _productRepository = productRepository;
            _cacheService = cacheService;
        }

        public async Task<DeleteProductCommandResponse> Handle(DeleteProductCommandRequest request, CancellationToken cancellationToken)
        {


           var product=await _productRepository.GetByIdAsync(request.Id,cancellationToken);
            if (product==null)
            {
                throw new Exception("Böyle bir veri bulunamadı"); 
            }
            await _cacheService.DeleteAsync(product.Id, cancellationToken);

            await _productRepository.RemoveAsync(product, cancellationToken);

            return new()
            {
                 Message="Başarılı"
            };
        }
    }
}
