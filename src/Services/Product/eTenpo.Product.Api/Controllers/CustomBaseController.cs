using Microsoft.AspNetCore.Mvc;

namespace eTenpo.Product.Api.Controllers;

// TODO: !! move controllers to presentation layer (new layer) to prevent DI of e.g. DbContext
// see details in Milan Jovanovic Patreon "Clean Architecture With .NET 6 And CQRS | Project Setup"

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[Produces("application/json")]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
[ProducesResponseType(StatusCodes.Status403Forbidden)]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public class CustomBaseController : ControllerBase { }