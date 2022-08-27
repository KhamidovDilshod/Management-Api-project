using Dinner.Domain.Entities;

namespace Dinner.Application.Common.Services.Authentication.Common;

public record AuthenticationResult(
    User user,
    string Token
);