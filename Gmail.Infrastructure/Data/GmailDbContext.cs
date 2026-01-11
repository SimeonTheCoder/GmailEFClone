using DotNetEnv;
using Gmail.Infrastructure.Common;
using Gmail.Infrastructure.Data.Configurations;
using Gmail.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Gmail.Infrastructure.Data
{
    public class GmailDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            Env.Load();
            base.OnConfiguring(optionsBuilder.UseSqlServer(Env.GetString("CONNECTION_STRING")));
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
