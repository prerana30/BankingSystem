using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BankingSystem.API.DTO
{
    public class AccountDTO
    {
        public Guid UserId { get; set; }
        
        //public long AccountNumber { get; set; }
        public decimal Balance { get; set; }
        //public long AtmCardNum { get; set; }

        public int AtmCardPin { get; set; }


    }
}
