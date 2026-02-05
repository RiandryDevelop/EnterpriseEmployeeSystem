using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EES.API.Controllers;

/// <summary>
/// Abstract base controller to provide common functionality for all API endpoints.
/// This class simplifies the use of MediatR by providing a centralized Mediator instance.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public abstract class BaseApiController : ControllerBase
{
    private IMediator? _mediator;

    /// <summary>
    /// Gets the Mediator instance from the request services.
    /// Uses lazy-loading to ensure the service is only resolved when needed.
    /// </summary>
    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<IMediator>();
}