using eTenpo.Product.Api.Requests.Category;
using eTenpo.Product.Application.CategoryFeature.Create;
using eTenpo.Product.Application.CategoryFeature.Delete;
using eTenpo.Product.Application.CategoryFeature.Read.All;
using eTenpo.Product.Application.CategoryFeature.Read.Single;
using eTenpo.Product.Application.CategoryFeature.Update;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eTenpo.Product.Api.Controllers;

[Asp.Versioning.ApiVersion("1.0")]
public class CategoriesController(IMediator mediator, ILogger<CategoriesController> logger) : BaseApiController
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CreateCategoryCommandResponse>> Create([FromBody] CreateCategoryRequest request)
    {
        logger.LogInformation("The create endpoint was triggered");
        logger.LogDebug("With the parameter {@Parameter}", request);

        var command = new CreateCategoryCommand(request.Name, request.Description);
        
        var response = await mediator.Send(command);

        logger.LogInformation("The category was created successfully");
        logger.LogDebug("Returning with the created category {@Category}", response);
        
        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<GetAllCategoriesRequestResponse>>> GetAll()
    {
        logger.LogInformation("The getAll endpoint was triggered");

        return Ok(await mediator.Send(new GetAllCategoriesRequest()));
    }
    
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetSingleCategoryRequestResponse>> GetById(Guid id)
    {
        logger.LogInformation("The getById endpoint was triggered");
        logger.LogDebug("With the parameter {Parameter}", id);
        
        return Ok(await mediator.Send(new GetSingleCategoryRequest(id)));
    }
    
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UpdateCategoryCommandResponse>> Update(Guid id, [FromBody] UpdateCategoryRequest request)
    {
        logger.LogInformation("The update endpoint was triggered");
        logger.LogDebug("With id {Id} and parameter {@Parameter}", id, request);

        var command = new UpdateCategoryCommand(id, request.Name, request.Description);

        var response = await mediator.Send(command);

        logger.LogInformation("The category was updated successfully");
        logger.LogDebug("Returning with the updated category {@Category}", response);
        
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
        
        await mediator.Send(new DeleteCategoryCommand(id));

        logger.LogInformation("The category was deleted successfully");
        
        return NoContent();
    }
}