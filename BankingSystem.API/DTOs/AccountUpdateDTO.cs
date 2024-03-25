using System.ComponentModel.DataAnnotations;

namespace BankingSystem.API.DTOs
{
    public class AccountUpdateDTO
    {
        [Required]
        [Range(1000, 9999, ErrorMessage = "Number must be a four-digit integer.")]
        public int AtmCardPin { get; set; }

        public AccountUpdateDTO()
        {

        }
    }
}
