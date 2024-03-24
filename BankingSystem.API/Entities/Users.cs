using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BankingSystem.API.Models
{

/* Users class inherits all the properties and functionality of IdentityUser<Guid>*/
    public class Users : IdentityUser<Guid>
    { 
        [Required]
        [MaxLength(50)]
        public string Fullname { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ModifiedAt { get; set; } = DateTime.UtcNow;

        public Users()
        {

        }
    }
}
