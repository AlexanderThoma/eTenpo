using AutoMapper;
using eTenpo.Product.Api.Dtos.Product;
using eTenpo.Product.Application.ProductFeature.Create;
using eTenpo.Product.Application.ProductFeature.Delete;
using eTenpo.Product.Application.ProductFeature.Read;
using eTenpo.Product.Application.ProductFeature.Update;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eTenpo.Product.Api.Controllers;

public class ProductsController : CustomBaseController
{
    private readonly ILogger<ProductsController> logger;
    private readonly IMediator mediator;
    private readonly IMapper mapper;

    public ProductsController(ILogger<ProductsController> logger, IMediator mediator, IMapper mapper)
    {
        this.logger = logger;
        this.mediator = mediator;
        this.mapper = mapper;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ProductCreateResponse>> Create([FromBody] CreateProductDto dto)
    {
        var command = this.mapper.Map<ProductCreateCommand>(dto);
        
        var response = await this.mediator.Send(command);

        return this.CreatedAtAction(nameof(GetSingle), new { id = response.Id });
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<ProductGetAllResponse>>> GetAll()
    {
        return this.Ok(await this.mediator.Send(new ProductGetAllRequest()));
    }
    
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductGetSingleResponse>> GetSingle(Guid id)
    {
        return this.Ok(await this.mediator.Send(new ProductGetSingleRequest(id)));
    }
    
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductUpdateResponse>> Update(Guid id, [FromBody] UpdateProductDto dto)
    {
        var command = this.mapper.Map<ProductUpdateCommand>(dto, options => options.AfterMap((src, dest) => dest.Id = id));

        var response = await this.mediator.Send(command);

        return this.Ok(response);
    }
    
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(Guid id)
    {
        await this.mediator.Send(new ProductDeleteCommand(id));

        return this.NoContent();
    }
}