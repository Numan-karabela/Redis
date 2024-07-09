 
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Redis.ProductFeatures;

namespace CacheNQueue.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = "Admin")]
    public class ProductController : ControllerBase
    {
        readonly IMediator mediator; 

        public ProductController(IMediator mediator)
        {
            this.mediator = mediator; 
        }
        [HttpPut]
        public async Task<IActionResult>Add(CreateProductCommandReques reques)
        {
          CreateProductCommandResponse response= await mediator.Send(reques);
           return Ok(response);

        }
        [HttpPut("DeleteId")]
        public async Task<IActionResult> Delete(DeleteProductCommandRequest request)
        {

            DeleteProductCommandResponse response = await mediator.Send(request);
            return Ok(response);
        }

        [HttpPut("UpdateId")]
        public async Task<IActionResult> Update(UpdateProductCommandRequest request)
        {
            ProductUpdateCommandResponse response = await mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("Gett")]
        public async Task<IActionResult> GettAll()
        {
            GettAllProductQueryRequest request = new GettAllProductQueryRequest();

            return Ok(await mediator.Send(request));
        }
        [HttpGet("Id")]
        public async Task<IActionResult> GettById(Guid id)
        {
            GetByIdProductQueryRequest request1 = new() { Id=id};
            GetByIdProductQueryResponse response= await mediator.Send(request1);
            return Ok(response);
        }

    


    }
}
