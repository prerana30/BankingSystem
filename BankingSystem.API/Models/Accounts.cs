using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BankingSystem.API.Models
{
    public class Accounts
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AccountId { get; set; }
        [Required]
        //[ForeignKey]
        public int UserId { get; set; }

        [Required]
        public long AccountNumber { get; set; }

        public long Balance { get; set; }
        public long AtmCardNum { get; set; }

        [Required]
        
        public int AtmCardPin { get; set; }

        public DateTime AccountCreatedAt { get; set; } = DateTime.Now;
        
        [Required]
        public int AccountCreatedBy { get; set; }

        public DateTime AccountModifiedAt { get; set; } = DateTime.Now;

        public int AccountModifiedBy { get; set; }

        public Accounts(int accountId, int userId, long balance, long atmCardNum, int atmCardPin, DateTime accountCreatedAt, int accountCreatedBy, DateTime accountModifiedAt, int accountModifiedBy)
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
