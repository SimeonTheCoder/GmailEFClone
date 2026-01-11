using Gmail.Core.Contracts;
using Gmail.Core.Exceptions;
using Gmail.Core.Models.Mail;
using Gmail.Infrastructure.Common;
using Gmail.Infrastructure.Data.Models;

namespace Gmail.Core.Services
{
    public class MailService : IMailService
    {
        private Repository repository;

        public MailService(Repository repository)
        {
            this.repository = repository;
        }

        public void Add(MailDTO data)
        {
            List<EmailAddress> existingEmails = repository.All<EmailAddress>().ToList();

            EmailAddress senderMail = existingEmails.FirstOrDefault(e => e.Address == data.SenderMail);

            if (senderMail == null)
                throw new NotFoundException($"Sender email ({data.SenderMail}) not found");
            
            List<MailRecipient> recipientsList = new();

            Mail mail = new()
            {
                Sender = senderMail,
                Subject = data.Subject,
                Content = data.Content
            };

            foreach (string recipientMail in data.Recipients)
            {
                EmailAddress currAddress = existingEmails.FirstOrDefault(e => e.Address == recipientMail);

                if (currAddress == null)
                    throw new NotFoundException($"Recipient email ({recipientMail}) not found");

                recipientsList.Add(
                    new MailRecipient()
                    {
                        Address = currAddress,
                        Mail = mail
                    }
                );
            }

            mail.Recipients = recipientsList;

            repository.Add<Mail>(mail);
            repository.SaveChanges();
        }

        public List<Mail> GetAll()
        {
            return repository.All<Mail>().ToList();
        }

        public List<Mail> GetAllAsNoTracking()
        {
            return repository.AllAsNoTracking<Mail>().ToList();
        }

        public Mail GetById(string id)
        {
            return repository.GetById<Mail>(id);
        }
    }
}
