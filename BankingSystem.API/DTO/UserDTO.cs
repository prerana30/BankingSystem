using BankingSystem.API.Models;
using System.ComponentModel.DataAnnotations;

namespace BankingSystem.API.DTO
{
    public class UserDTO
    {
        [Required]
        [MaxLength(50)]
        public string Username { get; set; }
        [Required]
        [MaxLength(50)]
        public string Fullname { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.(com|net|org|gov|np|edu)$", ErrorMessage = "Invalid pattern.")]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public string Address { get; set; }
        public Roles UserType { get; set; }
        public DateTime DateOfBirth { get; set; }

        public UserDTO( string username, string fullname, string email, string password, string address, Roles userType, DateTime dateOfBirth)
        {
            Username = username;
            Fullname = fullname;
            Email = email;
            Password = password;
            Address = address;
            UserType = userType;
            DateOfBirth = dateOfBirth;
        }

        public UserDTO()
        {

        }

    }
}
