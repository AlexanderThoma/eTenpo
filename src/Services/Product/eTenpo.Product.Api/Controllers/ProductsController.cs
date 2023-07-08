using AutoMapper;
using eTenpo.Product.Api.Requests.Product;
using eTenpo.Product.Application.ProductFeature.Create;
using eTenpo.Product.Application.ProductFeature.Delete;
using eTenpo.Product.Application.ProductFeature.Read.All;
using eTenpo.Product.Application.ProductFeature.Read.Single;
using eTenpo.Product.Application.ProductFeature.Update;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eTenpo.Product.Api.Controllers;
// TODO: Add Logging
[Asp.Versioning.ApiVersion("1.0")]
public class ProductsController : BaseApiController
{
    private readonly IMediator mediator;
    private readonly IMapper mapper;

    public ProductsController(IMediator mediator, IMapper mapper)
    {
        this.mediator = mediator;
        this.mapper = mapper;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CreateProductCommandResponse>> Create([FromBody] CreateProductRequest request)
    {
        var command = this.mapper.Map<CreateProductCommand>(request);
        
        var response = await this.mediator.Send(command);

        return this.CreatedAtAction(nameof(GetSingle), new { id = response.Id });
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<GetAllProductsResponse>>> GetAll()
    {
        return this.Ok(await this.mediator.Send(new GetAllProductsRequest()));
    }
    
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetSingleProductResponse>> GetSingle(Guid id)
    {
        return this.Ok(await this.mediator.Send(new GetSingleProductRequest(id)));
    }
    
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UpdateProductResponse>> Update(Guid id, [FromBody] UpdateProductRequest request)
    {
        var command = this.mapper.Map<UpdateProductCommand>(request, options => options.AfterMap((_, dest) => dest.Id = id));

        var response = await this.mediator.Send(command);

        return this.Ok(response);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(Guid id)
    {
        await this.mediator.Send(new DeleteProductCommand(id));

        return this.NoContent();
    }
}