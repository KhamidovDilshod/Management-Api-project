using Dinner.Domain.Entities;

namespace Dinner.Application.Common.Interface.Persistence
{
    public interface IUserRepository
    {
        User? GetUserByEmail(string email);
        void Add(User user);
    }
}