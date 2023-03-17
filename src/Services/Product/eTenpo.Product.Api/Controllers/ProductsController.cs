using AutoMapper;
using eTenpo.Product.Api.Application.ProductFeature.Create;
using eTenpo.Product.Api.Application.ProductFeature.Delete;
using eTenpo.Product.Api.Dtos.Product;
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
    public async Task<ActionResult> Create([FromBody] CreateProductDto dto)
    {
        var command = this.mapper.Map<ProductCreateCommand>(dto);
        
        var response = await this.mediator.Send(command);

        return this.CreatedAtAction(nameof(GetSingle), new { id = response.Id });
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> GetAll()
    {
        throw new NotImplementedException();
    }
    
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetSingle(Guid id)
    {
        throw new NotImplementedException();
    }
    
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Update(Guid id, [FromBody] UpdateProductDto dto)
    {
        throw new NotImplementedException();
    }
    
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(Guid id)
    {
        var response = await this.mediator.Send(new ProductDeleteCommand(id));

        return this.NoContent();
    }
}