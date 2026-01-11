using Gmail.Core.Contracts;
using Gmail.Core.Models.User;
using Gmail.Infrastructure.Common;
using Gmail.Infrastructure.Data.Models;

using static BCrypt.Net.BCrypt;

namespace Gmail.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository repository;

        public UserService(IRepository repository)
        {
            this.repository = repository;
        }

        public void Add(UserFormViewModel entity)
        {
            int usersWithSameEmail = repository.AllAsNoTracking<EmailAddress>()
                .Where(e => e.Address == entity.Email)
                .Count();

            if (repository.AllAsNoTracking<EmailAddress>()
                .Select(e => e.Address)
                .Contains(entity.Email))
            {
                throw new InvalidOperationException("User with such email already exists!");
            }

            EmailAddress address = new()
            {
                Address = entity.Email
            };

            repository.Add(address);

            User user = new()
            {
                Name = entity.Name,
                Email = address,
                PasswordHash = EnhancedHashPassword(entity.Password)
            };

            repository.Add(user);
            repository.SaveChanges();
        }
    }
}
