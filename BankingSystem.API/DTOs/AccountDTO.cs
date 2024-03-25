using System.ComponentModel.DataAnnotations;
namespace BankingSystem.API.DTOs
{
    public class AccountDTO
    {
        public Guid UserId { get; set; }
        public long AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public long AtmCardNum { get; set; }
        public int AtmCardPin { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Guid CreatedBy { get; set; }
        public DateTime ModifiedAt { get; set; } = DateTime.UtcNow;
        public Guid ModifiedBy { get; set; }

    }
}