using System.ComponentModel.DataAnnotations;

namespace Gmail.Core.Models.User
{
    public class UserCredentials
    {
        [RegularExpression("[A-Za-z ]+")]
        public string Name { get; set; } = string.Empty;

        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(250, MinimumLength = 5)]
        public string Password { get; set; } = string.Empty;
    }
}
