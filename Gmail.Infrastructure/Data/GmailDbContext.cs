using Gmail.Infrastructure.Common;
using Gmail.Infrastructure.Data.Configurations;
using Gmail.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Gmail.Infrastructure.Data
{
    public class GmailDbContext : DbContext
    {
        private string ConnectionString;

        public GmailDbContext(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder.UseSqlServer(ConnectionString));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new MailRecipientConfiguration());
            modelBuilder.ApplyConfiguration(new InboxMailConfiguration());
        }

        public DbSet<User> Users { get; set; }
        public DbSet<EmailAddress> EmailAddresses { get; set; }
        public DbSet<Mail> Mails { get; set; }
        public DbSet<InboxMail> InboxMails { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<MailRecipient> MailRecipients { get; set; }
    }
}
