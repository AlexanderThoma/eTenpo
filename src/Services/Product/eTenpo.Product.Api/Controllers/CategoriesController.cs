using AutoMapper;
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
public class CategoriesController : BaseApiController
{
    private readonly IMediator mediator;
    private readonly IMapper mapper;
    private readonly ILogger<CategoriesController> logger;

    public CategoriesController(IMediator mediator, IMapper mapper, ILogger<CategoriesController> logger)
    {
        this.mediator = mediator;
        this.mapper = mapper;
        this.logger = logger;
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CreateCategoryCommandResponse>> Create([FromBody] CreateCategoryRequest request)
    {
        this.logger.LogInformation("The create endpoint was triggered");
        this.logger.LogDebug("With the parameter {@Parameter}", request);
        
        var command = this.mapper.Map<CreateCategoryCommand>(request);
        
        var response = await this.mediator.Send(command);

        this.logger.LogInformation("The category was created successfully");
        this.logger.LogDebug("Returning with the created category {@Category}", response);
        
        return this.CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<GetAllCategoriesRequestResponse>>> GetAll()
    {
        this.logger.LogInformation("The getAll endpoint was triggered");

        return this.Ok(await this.mediator.Send(new GetAllCategoriesRequest()));
    }
    
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetSingleCategoryRequestResponse>> GetById(Guid id)
    {
        this.logger.LogInformation("The getById endpoint was triggered");
        this.logger.LogDebug("With the parameter {Parameter}", id);
        
        return this.Ok(await this.mediator.Send(new GetSingleCategoryRequest(id)));
    }
    
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UpdateCategoryCommandResponse>> Update(Guid id, [FromBody] UpdateCategoryRequest request)
    {
        this.logger.LogInformation("The update endpoint was triggered");
        this.logger.LogDebug("With id {Id} and parameter {@Parameter}", id, request);
        
        var command = this.mapper.Map<UpdateCategoryCommand>(request, options => options.AfterMap((_, dest) => dest.Id = id));

        var response = await this.mediator.Send(command);

        this.logger.LogInformation("The category was updated successfully");
        this.logger.LogDebug("Returning with the updated category {@Category}", response);
        
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
        
        await this.mediator.Send(new DeleteCategoryCommand(id));

        this.logger.LogInformation("The category was deleted successfully");
        
        return this.NoContent();
    }
}