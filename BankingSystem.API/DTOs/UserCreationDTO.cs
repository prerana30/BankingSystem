using BankingSystem.API.Entities;
using System.ComponentModel.DataAnnotations;

namespace BankingSystem.API.DTOs
{
    public class UserCreationDTO
    {
        [Required]
        [MaxLength(50)]
        public string UserName { get; set; }
        [Required]
        [MaxLength(50)]
        public string Fullname { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.(com|net|org|gov|np|edu)$", ErrorMessage = "Invalid pattern.")]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [MaxLength(10)]
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public UserRoles UserType { get; set; }
        public DateTime DateOfBirth { get; set; }

        public UserCreationDTO(string username, string fullname, string email, string password, string address, DateTime dateOfBirth)
        {
            UserName = username;
            Fullname = fullname;
            Email = email;
            Password = password;
            Address = address;
            DateOfBirth = dateOfBirth;
        }

        public UserCreationDTO()
        {

        }

    }
}
