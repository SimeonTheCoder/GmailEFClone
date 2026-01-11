using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Gmail.Infrastructure.Data.Constants.DbConstants.UserConstants;

namespace Gmail.Infrastructure.Data.Models
{
    public class User
    {
        public User()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Key]
        public string Id { get; set; }

        [Required]
        [ForeignKey(nameof(EmailId))]
        public EmailAddress Email { get; set; }
        public string EmailId { get; set; }

        [MaxLength(MaxNameLength)]
        public string Name { get; set; }

        [Required]
        public string PasswordHash { get; set; }
    }
}
