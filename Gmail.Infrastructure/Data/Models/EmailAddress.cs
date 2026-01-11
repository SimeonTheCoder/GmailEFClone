using System.ComponentModel.DataAnnotations;

using static Gmail.Infrastructure.Data.Constants.DbConstants.EmailAddressConstants;

namespace Gmail.Infrastructure.Data.Models
{
    public class EmailAddress
    {
        public EmailAddress()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Key]
        [Required]
        public string Id { get; set; }

        [Required]
        [MaxLength(MaxEmailLength)]
        public string Address { get; set; }
    }
}
