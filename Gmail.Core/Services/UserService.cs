using BCrypt.Net;
using Gmail.Core.Contracts;
using Gmail.Core.Contracts.Common;
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

        public void Add(UserCredentials credentials)
        {
            int usersWithSameEmail = repository.AllAsNoTracking<EmailAddress>()
                .Where(e => e.Address == credentials.Email)
                .Count();

            if (repository.AllAsNoTracking<EmailAddress>()
                .Select(e => e.Address)
                .Contains(credentials.Email))
            {
                throw new InvalidOperationException("User with such email already exists!");
            }

            EmailAddress address = new()
            {
                Address = credentials.Email
            };

            repository.Add(address);

            User user = new()
            {
                Name = credentials.Name,
                Email = address,
                PasswordHash = EnhancedHashPassword(credentials.Password)
            };

            repository.Add(user);
            repository.SaveChanges();
        }

        public List<User> GetAll()
        {
            return repository.All<User>().ToList();
        }

        public List<User> GetAllAsNoTracking()
        {
            return repository.AllAsNoTracking<User>().ToList();
        }

        public User GetById(string id)
        {
            return repository.GetById<User>(id);
        }

        public bool ValidateCredentials(UserCredentials credentials)
        {
            var user = GetAllAsNoTracking().FirstOrDefault(u => u.Email.Address == credentials.Email);

            if (user == null) return false;

            return EnhancedVerify(credentials.Password, user.PasswordHash);
        }
    }
}
