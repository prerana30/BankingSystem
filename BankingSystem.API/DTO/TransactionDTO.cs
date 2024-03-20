using BankingSystem.API.Models;
using System.ComponentModel.DataAnnotations;

namespace BankingSystem.API.DTO
{
    public class TransactionDTO
    {
        public TransactionType TransactionType { get; set; }

        public decimal Amount { get; set; }

        public DateTime TransactionTime { get; set; }

        public string ? TransactionRemarks { get; set; }

    }
}
