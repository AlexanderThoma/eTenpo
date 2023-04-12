using AutoMapper;
using eTenpo.Product.Api.Dtos.Product;
using eTenpo.Product.Application.ProductFeature.Create;
using eTenpo.Product.Application.ProductFeature.Delete;
using eTenpo.Product.Application.ProductFeature.Read;
using eTenpo.Product.Application.ProductFeature.Update;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eTenpo.Product.Api.Controllers;
// TODO: Add Logging
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
    public async Task<ActionResult<CreateProductCommandResponse>> Create([FromBody] CreateProductDto dto)
    {
        var command = this.mapper.Map<CreateProductCommand>(dto);
        
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
    public async Task<ActionResult<UpdateProductResponse>> Update(Guid id, [FromBody] UpdateProductDto dto)
    {
        var command = this.mapper.Map<UpdateProductCommand>(dto, options => options.AfterMap((src, dest) => dest.Id = id));

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