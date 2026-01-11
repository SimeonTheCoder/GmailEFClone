using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using static Gmail.Infrastructure.Data.Constants.DbConstants.MailConstants;

namespace Gmail.Infrastructure.Data.Models
{
    public class Mail
    {
        public Mail()
        {
            this.Id = Guid.NewGuid().ToString();
            this.DateSent = DateTime.UtcNow;

            this.Recipients = new();
        }

        [Key]
        public string Id { get; set; }

        [MaxLength(200)]
        public string Subject { get; set; } = string.Empty;

        [Required]
        [MaxLength(2000)]
        public string Content { get; set; } = string.Empty;

        [Required]
        public DateTime DateSent { get; set; }

        [Required]
        [ForeignKey(nameof(SenderId))]
        public virtual EmailAddress Sender { get; set; }
        public string SenderId { get; set; }

        [Required]
        public virtual List<MailRecipient> Recipients { get; set; }
    }
}
