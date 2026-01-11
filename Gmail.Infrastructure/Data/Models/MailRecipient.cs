using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gmail.Infrastructure.Data.Models
{
    public class MailRecipient
    {
        public MailRecipient()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Key]
        public string Id { get; set; }

        [Required]
        [ForeignKey(nameof(MailId))]
        public virtual Mail Mail { get; set; }
        public string MailId { get; set; }

        [Required]
        [ForeignKey(nameof(AddressId))]
        public virtual EmailAddress Address { get; set; }
        public string AddressId { get; set; }
    }
}
