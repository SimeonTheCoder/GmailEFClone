using System.ComponentModel.DataAnnotations;

namespace Gmail.Core.Models.Mail
{
    public class MailDTO
    {
        [MaxLength(200)]
        public string Subject { get; set; } = string.Empty;

        [Required]
        [MaxLength(2000)]
        public string Content { get; set; } = string.Empty;

        [Required]
        public string SenderMail { get; set; } = string.Empty;

        [Required]
        public List<string> Recipients { get; set; } = new();
    }
}
