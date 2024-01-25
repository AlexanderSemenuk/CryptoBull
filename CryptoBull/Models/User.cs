using System.ComponentModel.DataAnnotations;

namespace CryptoBull.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string firstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string lastName { get; set; }

        [Required]
        [MaxLength(255)]
        public string hashPassword { get; set; }

        [Required]
        [EmailAddress]
        public string email { get; set; }
    }
}
