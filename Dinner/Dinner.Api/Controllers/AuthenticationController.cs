using Dinner.Application.Services.Authentication;
using Dinner.Contracts.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Dinner.Api.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }
        [HttpPost("register")]
        public IActionResult Register(RegisterRequest request)
        {
            var authResult = _authenticationService.Register(request.FirstName, request.Password, request.Email, request.LastName);
            return Ok(authResult);
        }
        [HttpPost("login")]
        public IActionResult Login(LoginRequest request)
        {
            var authResult=_authenticationService.Login(request.Email, request.Password);
            return Ok(authResult);
        }
    }
}