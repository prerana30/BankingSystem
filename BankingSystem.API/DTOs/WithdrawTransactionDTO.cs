using BankingSystem.API.Models;

namespace BankingSystem.API.DTO
{
    public class WithdrawTransactionDTO
    {
        public TransactionType TransactionType = TransactionType.Withdraw;

        public decimal Amount { get; set; }

        public string ? TransactionRemarks { get; set; }

        public DateTime TransactionTime { get; set; }

    }
}
