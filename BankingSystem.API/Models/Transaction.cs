using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankingSystem.API.Models
{
    public class Transaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TransactionId { get; set; }

        [Required]//[ForeignKey]        
        public int AccountId { get; set; }

        public TransactionType TransactionType { get; set; }

        [Required]
        [MaxLength(50)]
        public double Amount { get; set; }

        public DateTime TransactionTime { get; set; }

        [MaxLength(150)]
        public string ? TransactionRemarks { get; set; }


        //public Transaction(int transactionId, int accountId, TransactionType transactionType, double amount, DateTime transactionTime, string transactionRemarks)
        //{
        //    TransactionId = transactionId;
        //    AccountId = accountId;            
        //    TransactionType = transactionType;
        //    Amount = amount;
        //    TransactionTime = transactionTime;
        //    TransactionRemarks = transactionRemarks;
        //}

        public Transaction()
        { }
    }
}
