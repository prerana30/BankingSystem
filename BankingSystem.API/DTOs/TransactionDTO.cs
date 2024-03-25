using BankingSystem.API.Entities;

namespace BankingSystem.API.DTOs
{
    public class TransactionDTO
    {
        public TransactionType TransactionType { get; set; }

        public decimal Amount { get; set; }

        public DateTime TransactionTime { get; set; }

        public string ? TransactionRemarks { get; set; }
    }
}
