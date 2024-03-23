using System.ComponentModel.DataAnnotations;
namespace BankingSystem.API.DTO
{
    public class AccountDTO
    {
        public Guid UserId { get; set; }
        
<<<<<<< HEAD
        //public long AccountNumber { get; set; }
=======
        [Required]
        public long AccountNumber { get; set; }
>>>>>>> c9515923c7f2aef77a48c0553fe8d43457c1cbf9
        public decimal Balance { get; set; }
        public long AtmCardNum { get; set; }

<<<<<<< HEAD
        public int AtmCardPin { get; set; }

=======
        [Required]
        public int AtmCardPin { get; set; }
        public DateTime AccountCreatedAt { get; set; } = DateTime.UtcNow;
        [Required]
        public Guid AccountCreatedBy { get; set; }
        public DateTime AccountModifiedAt { get; set; } = DateTime.UtcNow;
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
>>>>>>> c9515923c7f2aef77a48c0553fe8d43457c1cbf9

        public AccountDTO(Guid id, long accountNumber, long balance, long atmCardNum, int atmCardPin, DateTime accountCreatedAt, Guid accountCreatedBy, DateTime accountModifiedAt, Guid accountModifiedBy)
        {
            UserId = id;
            AccountNumber = accountNumber;
            AtmCardNum = atmCardNum;
            Balance = balance;
            AtmCardPin = atmCardPin;
            AccountCreatedAt = accountCreatedAt;
            AccountCreatedBy = accountCreatedBy;
            AccountModifiedAt = accountModifiedAt;
            AccountModifiedBy = accountModifiedBy;
        }
    }
}