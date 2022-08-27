using Dinner.Application.Common.Services.Authentication.Commands;
using Dinner.Application.Common.Services.Authentication.Common;
using Dinner.Application.Common.Services.Authentication.Queries;
using Dinner.Contracts.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Dinner.Api.Controllers;

[ApiController]
[Route("auth")]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationCommandService _authenticationCommandService;
    private readonly IAuthenticationQueryService _authenticationQueryService;

    public AuthenticationController(
        IAuthenticationCommandService authenticationCommandService,
        IAuthenticationQueryService authenticationQueryService)
    {
        _authenticationQueryService = authenticationQueryService;
        _authenticationCommandService = authenticationCommandService;
    }

    [HttpPost("register")]
    public IActionResult Register(RegisterRequest request)
    {
        var authResult = _authenticationCommandService.Register(
            request.FirstName,
            request.Password,
            request.Email,
            request.LastName);
        return authResult.MatchFirst(
            result => Ok(MapAuthResult(result)),
            problem => Problem(statusCode: StatusCodes.Status409Conflict, title: problem.Description)
        );
    }


    private static AuthenticationResponse MapAuthResult(AuthenticationResult authResult)
    {
        return new AuthenticationResponse(
            authResult.user.Id,
            authResult.user.FirstName,
            authResult.user.LastName,
            authResult.user.Email,
            authResult.Token
        );
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequest request)
    {
        var authResult = _authenticationQueryService.Login(
            request.Email,
            request.Password);
        return authResult.MatchFirst(
            result => Ok(MapAuthResult(result)),
            errors => Problem(errors.Description));
    }
}