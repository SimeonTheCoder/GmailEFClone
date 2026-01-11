using Gmail.Infrastructure.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gmail.Infrastructure.Data.Models
{
    public class Attachment
    {
        public Attachment()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Key]
        public string Id { get; set; }

        [Required]
        public AttachmentType Type { get; set; }

        [Required]
        public int Filesize { get; set; }

        [Required]
        [MaxLength(500)]
        public string URL { get; set; }

        [Required]
        [ForeignKey(nameof(MailId))]
        public virtual Mail Mail { get; set; }
        public string MailId { get; set; }
    }
}
