using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gmail.Infrastructure.Data.Models
{
    public class InboxMail
    {
        public InboxMail()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Key]
        public string Id { get; set; }

        [Required]
        public bool IsRead { get; set; } = false;

        [Required]
        public bool IsImportant { get; set; } = false;

        [Required]
        public bool IsStarred { get; set; } = false;

        [Required]
        public bool IsSent { get; set; } = false;

        [Required]
        public bool IsSpam { get; set; } = false;

        [Required]
        public bool IsDraft { get; set; } = false;

        [Required]
        [ForeignKey(nameof(AddressId))]
        public virtual EmailAddress Address { get; set; }
        public string AddressId { get; set; }

        [Required]
        [ForeignKey(nameof(MailId))]
        public virtual Mail Mail { get; set; }
        public string MailId { get; set; }
    }
}
