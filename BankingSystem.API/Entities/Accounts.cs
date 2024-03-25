using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BankingSystem.API.Entities
{
    public class Accounts
    {
        [Key]
        public Guid AccountId { get; set; }
        [Required]
        public Guid UserId { get; set; }

        // Navigation property
        [ForeignKey("UserId")]
        public Users User { get; set; }

        [Required]
        public long AccountNumber { get; set; }

        public decimal Balance { get; set; }
        public long AtmCardNum { get; set; }

        [Required]
        public int AtmCardPin { get; set; }

        [Required]
        public Guid CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Guid? ModifiedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
}
