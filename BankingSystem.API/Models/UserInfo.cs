using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankingSystem.API.Models
{
    public class UserInfo
    {
        [Key]
        public Guid UserInfoId { get; set; } = Guid.NewGuid();
        [Required]
        public Guid AspNetUserId { get; set; }
        // Navigation property
        [ForeignKey("Id")]
        public Users User { get; set; }
        [Required]
        [MaxLength(50)]
        public string UserName { get; set; }
        [Required]
        [MaxLength(50)]
        public string Fullname { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.(com|net|org|gov|np|edu)$", ErrorMessage = "Invalid pattern.")]
        public string Email { get; set; }
        [Required]
        public string PasswordHash { get; set; }
        [Required]
        [MaxLength(10)]
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string UserType { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ModifiedAt { get; set; } = DateTime.UtcNow;

        public UserInfo()
        {

        }
    }
}
