using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BankingSystem.API.DTO
{
    public class AccountDTO
    {
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

        public AccountDTO( long balance, long atmCardNum, int atmCardPin, DateTime accountCreatedAt, int accountCreatedBy, DateTime accountModifiedAt, int accountModifiedBy)
        {
            Balance = balance;
            AtmCardNum = atmCardNum;
            AtmCardPin = atmCardPin;
            AccountCreatedAt = accountCreatedAt;
            AccountCreatedBy = accountCreatedBy;
            AccountModifiedAt = accountModifiedAt;
            AccountModifiedBy = accountModifiedBy;

        }

    }
}
