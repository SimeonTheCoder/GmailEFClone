using Gmail.Core.Contracts;
using Gmail.Infrastructure.Common;
using Gmail.Infrastructure.Data.Models;

namespace Gmail.Core.Services
{
    public class InboxMailService : IInboxMailService
    {
        private Repository repository;

        public InboxMailService(Repository repository)
        {
            this.repository = repository;
        }

        public List<InboxMail> GetAll()
        {
            return repository.All<InboxMail>().ToList();
        }

        public List<InboxMail> GetAllAsNoTracking()
        {
            return repository.AllAsNoTracking<InboxMail>().ToList();
        }

        public InboxMail GetById(string id)
        {
            return repository.GetById<InboxMail>(id);
        }
    }
}
