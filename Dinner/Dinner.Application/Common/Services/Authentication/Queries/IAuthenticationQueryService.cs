using Dinner.Application.Common.Services.Authentication.Common;
using ErrorOr;

namespace Dinner.Application.Common.Services.Authentication.Queries;

public interface IAuthenticationQueryService
{
    ErrorOr<AuthenticationResult> Login(string email, string password);
}