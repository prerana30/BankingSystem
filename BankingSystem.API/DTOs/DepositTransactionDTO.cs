using BankingSystem.API.Models;

namespace BankingSystem.API.DTO
{
    public class DepositTransactionDTO
    {
        public TransactionType TransactionType = TransactionType.Deposit;

        public decimal Amount { get; set; }

        public string ? TransactionRemarks { get; set; }

        public DateTime TransactionTime { get; set; }

    }
}
