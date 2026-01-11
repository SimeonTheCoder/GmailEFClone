using Gmail.Core.Contracts.Common;
using Gmail.Core.Models.Mail;
using Gmail.Infrastructure.Data.Models;

namespace Gmail.Core.Contracts
{
    public interface IMailService : IAddable<MailDTO>, IReadable<Mail>, IReadableAll<Mail>
    {

    }
}
