namespace Dinner.Application.Services.Authentication
{
    public interface IAuthenticationService
    {
        AuthenticationResult Register(string firstName, string password, string email, string lastName);
        AuthenticationResult Login(string email, string password);
    }
}