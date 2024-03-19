using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BankingSystem.API.Models
{
    public class Accounts
    {
        [Key]
        public Guid AccountId { get; set; } = Guid.NewGuid();
        [Required]
        public Guid UserId { get; set; }

        // Navigation property
        [ForeignKey("UserId")]
        public Users User { get; set; }

        [Required]
        public long AccountNumber { get; set; }

        public long Balance { get; set; }
        public long AtmCardNum { get; set; }

        [Required]
        
        public int AtmCardPin { get; set; }

        public DateTime AccountCreatedAt { get; set; } = DateTime.UtcNow;
        
        [Required]
        public Guid AccountCreatedBy { get; set; }

        // Navigation property
        [ForeignKey("AccountCreatedBy")]
        public Users CreatedByUser { get; set; }

        public DateTime AccountModifiedAt { get; set; } = DateTime.UtcNow;

        public Guid AccountModifiedBy { get; set; }

        // Navigation property
        [ForeignKey("AccountModifiedBy")]
        public Users ModifiedByUser { get; set; }


        public Accounts(Guid accountId, Guid userId, long balance, long atmCardNum, int atmCardPin, DateTime accountCreatedAt, Guid accountCreatedBy, DateTime accountModifiedAt, Guid accountModifiedBy)
        {
            AccountId = accountId;
            UserId = userId;
            Balance = balance;
            AtmCardNum = atmCardNum;
            AtmCardPin = atmCardPin;
            AccountCreatedAt = accountCreatedAt;
            AccountCreatedBy = accountCreatedBy;
            AccountModifiedAt = accountModifiedAt;
            AccountModifiedBy = accountModifiedBy;
    
        }

        public Accounts()
        {

        }
    }
}
