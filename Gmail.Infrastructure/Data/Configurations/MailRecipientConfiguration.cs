using Gmail.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gmail.Infrastructure.Data.Configurations
{
    public class MailRecipientConfiguration : IEntityTypeConfiguration<MailRecipient>
    {
        public void Configure(EntityTypeBuilder<MailRecipient> builder)
        {
            builder
                .HasOne(mr => mr.Address)
                .WithMany()
                .HasForeignKey(mr => mr.AddressId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(mr => mr.Mail)
                .WithMany(m => m.Recipients)
                .HasForeignKey(mr => mr.MailId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
