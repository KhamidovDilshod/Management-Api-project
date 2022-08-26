using Dinner.Application.Common.Interface.Persistence;
using Dinner.Domain.Entities;

namespace Dinner.Infrastructure.Persistence
{
    public class UserRepository : IUserRepository
    {
        private static readonly List<User> _users = new List<User>();
        public User? GetUserByEmail(string email)
        {
            return _users.SingleOrDefault(u => u.Email == email);
        }
        public void Add(User user)
        {
            _users.Add(user);
        }
    }
}