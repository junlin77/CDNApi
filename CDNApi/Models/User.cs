using System.ComponentModel.DataAnnotations;

namespace CDNApi.Models 
{
    public class User
    {
        [Key]
        public int Id { get; set; } 
        
        [Required]
        [StringLength(50)]
        public string Username { get; set; } = null!;

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; } = null!;

        [Required]
        [Phone]
        [StringLength(15)]
        public string PhoneNumber { get; set; } = null!;

        [StringLength(200)]
        public string SkillSets { get; set; } = null!;

        [StringLength(200)]
        public string Hobby { get; set; } = null!;
    }
}
