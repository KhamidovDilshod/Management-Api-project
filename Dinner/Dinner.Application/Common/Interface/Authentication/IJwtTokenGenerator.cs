using Dinner.Domain.Entities;

namespace Dinner.Application.Common.Interface.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}