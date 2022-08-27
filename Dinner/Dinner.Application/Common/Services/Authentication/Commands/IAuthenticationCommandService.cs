using Dinner.Application.Common.Services.Authentication.Common;
using ErrorOr;

namespace Dinner.Application.Common.Services.Authentication.Commands;

public interface IAuthenticationCommandService
{
    ErrorOr<AuthenticationResult> Register(string firstName, string password, string email,
        string lastName);
}