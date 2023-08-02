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
[Asp.Versioning.ApiVersion("1.0")]
public class ProductsController : BaseApiController
{
    private readonly IMediator mediator;
    private readonly IMapper mapper;
    private readonly ILogger<ProductsController> logger;

    public ProductsController(IMediator mediator, IMapper mapper, ILogger<ProductsController> logger)
    {
        this.mediator = mediator;
        this.mapper = mapper;
        this.logger = logger;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CreateProductCommandResponse>> Create([FromBody] CreateProductRequest request)
    {
        this.logger.LogInformation("The create endpoint was triggered");
        this.logger.LogDebug("With the parameter {@Parameter}", request);
        
        var command = this.mapper.Map<CreateProductCommand>(request);
        
        var response = await this.mediator.Send(command);

        this.logger.LogInformation("The product was created successfully");
        this.logger.LogDebug("Returning with the created product {@Product}", response);
        
        return this.CreatedAtAction(nameof(GetById), new { id = response.Id });
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<GetAllProductsResponse>>> GetAll()
    {
        this.logger.LogInformation("The getAll endpoint was triggered");

        return this.Ok(await this.mediator.Send(new GetAllProductsRequest()));
    }
    
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetSingleProductResponse>> GetById(Guid id)
    {
        this.logger.LogInformation("The getById endpoint was triggered");
        this.logger.LogDebug("With the parameter {Parameter}", id);
        
        return this.Ok(await this.mediator.Send(new GetSingleProductRequest(id)));
    }
    
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UpdateProductResponse>> Update(Guid id, [FromBody] UpdateProductRequest request)
    {
        this.logger.LogInformation("The update endpoint was triggered");
        this.logger.LogDebug("With id {Id} and parameter {@Parameter}", id, request);
        
        var command = this.mapper.Map<UpdateProductCommand>(request, options => options.AfterMap((_, dest) => dest.Id = id));

        var response = await this.mediator.Send(command);

        this.logger.LogInformation("The product was updated successfully");
        this.logger.LogDebug("Returning with the updated product {@Product}", response);
        
        return this.Ok(response);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(Guid id)
    {
        this.logger.LogInformation("The delete endpoint was triggered");
        this.logger.LogDebug("With id {Id}", id);
        
        await this.mediator.Send(new DeleteProductCommand(id));

        this.logger.LogInformation("The product was deleted successfully");
        
        return this.NoContent();
    }
}