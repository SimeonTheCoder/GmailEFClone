using Gmail.Core.Contracts.Common;
using Gmail.Core.Models.User;
using Gmail.Infrastructure.Data.Models;

namespace Gmail.Core.Contracts
{
    public interface IUserService : IAddable<UserCredentials>, IReadable<User>, IReadableAll<User>
    {
        
    }
}
