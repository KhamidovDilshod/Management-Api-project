using Dinner.Application.Common.Interface.Authentication;
using Dinner.Application.Common.Interface.Persistence;
using Dinner.Domain.Entities;
#pragma warning disable
namespace Dinner.Application.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;

        public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }

        AuthenticationResult IAuthenticationService.Register(string firstName, string password, string email, string lastName)
        {
            // 1) Validate the user doesn't already exist
            if (_userRepository.GetUserByEmail(email) != null)
            {
                throw new Exception("User already exists");
            }
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

        AuthenticationResult IAuthenticationService.Login(string email, string password)
        {
            // 1. Validate the user exists
            if (_userRepository.GetUserByEmail(email) is not User user)
            {
                throw new Exception("User does not exist");
            }
            // 2. Validate the password is correct\
            if (_userRepository.GetUserByEmail(email).Password != password)
            {
                throw new Exception("Password is incorrect");
            }
            // 3. Generate JWT Token
            string token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(
                user,
                token);
        }
    }
}