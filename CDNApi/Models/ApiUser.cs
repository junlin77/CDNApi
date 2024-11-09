using System.ComponentModel.DataAnnotations;

namespace CDNApi.Models 
{
    public class ApiUser
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; } = null!;

        [Required]
        [StringLength(200)]
        public string PasswordHash { get; set; } = null!;

        // Refresh token details
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryDate { get; set; }
    }

}