using Dinner.Application.Common.Interface.Authentication;
using Dinner.Application.Common.Interface.Persistence;
using Dinner.Application.Common.Services.Authentication.Common;
using Dinner.Domain.Common.Errors;
using Dinner.Domain.Entities;
using ErrorOr;

#pragma warning disable
namespace Dinner.Application.Common.Services.Authentication.Queries;

public class AuthenticationQueryService : IAuthenticationQueryService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public AuthenticationQueryService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }

    public ErrorOr<AuthenticationResult> Login(string email, string password)
    {
        // 1. Validate the user exists
        if (_userRepository.GetUserByEmail(email) is not User user)
            return CustomError.Authentication.InvalidCredentials;

        // 2. Validate the password is correct
        if (_userRepository.GetUserByEmail(email).Password != password)
            return CustomError.Authentication.InvalidCredentials;

        // 3. Generate JWT Token
        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(
            user,
            token);
    }
}