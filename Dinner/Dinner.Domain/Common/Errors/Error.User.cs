using ErrorOr;

namespace Dinner.Domain.Common.Errors;

public static partial class CustomError
{
    public static class User
    {
        public static Error DuplicateEmail => Error.Conflict(
            "User.DuplicateEmail",
            "Email is already in use");
    }
}