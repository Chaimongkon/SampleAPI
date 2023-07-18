using System.ComponentModel.DataAnnotations;

namespace SampleAPI.Models
{
    public class User
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        public string? Fullname { get; set; }

        [Required]
        public string Email { get; set; } = string.Empty;

        public string? Token { get; set; }

        public string? SaltHash { get; set; }
    }
}
