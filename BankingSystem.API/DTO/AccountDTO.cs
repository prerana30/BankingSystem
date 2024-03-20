using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BankingSystem.API.DTO
{
    public class AccountDTO
    {
        public Guid UserId { get; set; }
        
        [Required]
        //public long AccountNumber { get; set; }
        public decimal Balance { get; set; }
        //public long AtmCardNum { get; set; }

        [Required]
        
        public int AtmCardPin { get; set; }

        public DateTime AccountCreatedAt { get; set; } = DateTime.Now;

        [Required]
        public Guid AccountCreatedBy { get; set; }

        public DateTime AccountModifiedAt { get; set; } = DateTime.Now;

        public Guid AccountModifiedBy { get; set; }

        public AccountDTO( Guid userId, decimal balance, int atmCardPin, DateTime accountCreatedAt, Guid accountCreatedBy, DateTime accountModifiedAt, Guid accountModifiedBy)
        {
            UserId = userId;
            Balance = balance;
            AtmCardPin = atmCardPin;
            AccountCreatedAt = accountCreatedAt;
            AccountCreatedBy = accountCreatedBy;
            AccountModifiedAt = accountModifiedAt;
            AccountModifiedBy = accountModifiedBy;

        }

    }
}
