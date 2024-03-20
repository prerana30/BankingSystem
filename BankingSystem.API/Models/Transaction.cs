using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankingSystem.API.Models
{
    public class Transaction
    {
        [Key]
        public Guid TransactionId { get; set; } = Guid.NewGuid();

        [Required]
        public Guid AccountId { get; set; }

        // Navigation property
        [ForeignKey("AccountId")]
        public Accounts Accounts { get; set; }

        public TransactionType TransactionType { get; set; }

        [Required]
        [MaxLength(50)]
        public double Amount { get; set; }

        public DateTime TransactionTime { get; set; }= DateTime.UtcNow;

        [MaxLength(150)]
        public string ? TransactionRemarks { get; set; }


        public Transaction()
        { }
    }
}
