using ErrorOr;

namespace Dinner.Domain.Common.Errors;

public static partial class CustomError
{
    public static class Authentication
    {
        public static Error InvalidCredentials => Error.Validation(
            "Auth.InvalidCred",
            "Invalid Credentials"
        );
    }
}