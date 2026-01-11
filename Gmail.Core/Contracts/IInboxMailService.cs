using Gmail.Core.Contracts.Common;
using Gmail.Infrastructure.Data.Models;

namespace Gmail.Core.Contracts
{
    public interface IInboxMailService : IReadable<InboxMail>, IReadableAll<InboxMail>
    {

    }
}
