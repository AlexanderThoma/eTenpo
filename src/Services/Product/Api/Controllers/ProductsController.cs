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
public class ProductsController(IMediator mediator, ILogger<ProductsController> logger) : BaseApiController
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CreateProductCommandResponse>> Create([FromBody] CreateProductRequest request)
    {
        logger.LogInformation("The create endpoint was triggered");
        logger.LogDebug("With the parameter {@Parameter}", request);

        var command = new CreateProductCommand(
            request.Name,
            request.Price,
            request.Description,
            request.AvailableStock,
            request.CategoryId);
        
        var response = await mediator.Send(command);

        logger.LogInformation("The product was created successfully");
        logger.LogDebug("Returning with the created product {@Product}", response);
        
        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<GetAllProductsRequestResponse>>> GetAll()
    {
        logger.LogInformation("The getAll endpoint was triggered");

        return Ok(await mediator.Send(new GetAllProductsRequest()));
    }
    
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetSingleProductRequestResponse>> GetById(Guid id)
    {
        logger.LogInformation("The getById endpoint was triggered");
        logger.LogDebug("With the parameter {Parameter}", id);
        
        return Ok(await mediator.Send(new GetSingleProductRequest(id)));
    }
    
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UpdateProductCommandResponse>> Update(Guid id, [FromBody] UpdateProductRequest request)
    {
        logger.LogInformation("The update endpoint was triggered");
        logger.LogDebug("With id {Id} and parameter {@Parameter}", id, request);

        var command = new UpdateProductCommand(id, request.Name, request.Price, request.Description, request.CategoryId);

        var response = await mediator.Send(command);

        logger.LogInformation("The product was updated successfully");
        logger.LogDebug("Returning with the updated product {@Product}", response);
        
        return Ok(response);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(Guid id)
    {
        logger.LogInformation("The delete endpoint was triggered");
        logger.LogDebug("With id {Id}", id);
        
        await mediator.Send(new DeleteProductCommand(id));

        logger.LogInformation("The product was deleted successfully");
        
        return NoContent();
    }
    
    /*[HttpPut("{id:guid}/addstock")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UpdateProductCommandResponse>> Update(Guid id, [FromBody] UpdateProductRequest request)
    {
        logger.LogInformation("The update endpoint was triggered");
        logger.LogDebug("With id {Id} and parameter {@Parameter}", id, request);
        
        var command = mapper.Map<UpdateProductCommand>(request, options => options.AfterMap((_, dest) => dest.Id = id));

        var response = await mediator.Send(command);

        logger.LogInformation("The product was updated successfully");
        logger.LogDebug("Returning with the updated product {@Product}", response);
        
        return Ok(response);
    }*/
}