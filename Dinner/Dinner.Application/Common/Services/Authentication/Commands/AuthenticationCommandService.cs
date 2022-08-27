using Dinner.Application.Common.Interface.Authentication;
using Dinner.Application.Common.Interface.Persistence;
using Dinner.Application.Common.Services.Authentication.Common;
using Dinner.Domain.Common.Errors;
using Dinner.Domain.Entities;
using ErrorOr;

#pragma warning disable
namespace Dinner.Application.Common.Services.Authentication.Commands;

public class AuthenticationCommandService : IAuthenticationCommandService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public AuthenticationCommandService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }

    public ErrorOr<AuthenticationResult> Register(string firstName, string password, string email,
        string lastName)
    {
        // 1) Validate the user doesn't already exist
        if (_userRepository.GetUserByEmail(email) != null) return CustomError.User.DuplicateEmail;

        // 2. Create user (generate unique ID) & Persist to DB
        var user = new User
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            Password = password
        };
        // 3. Generate JWT Token
        _userRepository.Add(user);

        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(
            user,
            token
        );
    }
}