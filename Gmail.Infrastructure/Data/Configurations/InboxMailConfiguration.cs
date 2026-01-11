using Gmail.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gmail.Infrastructure.Data.Configurations
{
    public class InboxMailConfiguration : IEntityTypeConfiguration<InboxMail>
    {
        public void Configure(EntityTypeBuilder<InboxMail> builder)
        {
            builder
                .HasIndex(um => new { um.AddressId, um.MailId })
                .IsUnique();

            builder
                .HasOne(um => um.Address)
                .WithMany()
                .HasForeignKey(um => um.AddressId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(um => um.Mail)
                .WithMany()
                .HasForeignKey(um => um.MailId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
