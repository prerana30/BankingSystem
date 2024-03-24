using BankingSystem.API.Models;
using System.ComponentModel.DataAnnotations;

namespace BankingSystem.API.DTO
{
    public class UserUpdateDTO
    {
        public string UserName { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }

        public UserUpdateDTO()
        {

        }
    }
}
