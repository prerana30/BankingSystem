using BankingSystem.API.Models;
using System.ComponentModel.DataAnnotations;

namespace BankingSystem.API.DTO
{
    public class AccountUpdateDTO
    {
        public int AtmCardPin { get; set; }

        public AccountUpdateDTO()
        {

        }
    }
}
