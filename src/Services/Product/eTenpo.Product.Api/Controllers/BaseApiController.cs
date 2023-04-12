using Microsoft.AspNetCore.Mvc;

namespace eTenpo.Product.Api.Controllers;

[ApiController]
// TODO: find samples how to use api-version correctly
[Route("api/v{version:apiVersion}/[controller]")]
[Produces("application/json")]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
[ProducesResponseType(StatusCodes.Status403Forbidden)]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public class BaseApiController : ControllerBase { }