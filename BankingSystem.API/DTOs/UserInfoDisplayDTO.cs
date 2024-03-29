using System.ComponentModel.DataAnnotations;

namespace BankingSystem.API.DTOs
{
    public class UserInfoDisplayDTO
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string UserType { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public string? JWTtoken { get; set; }
        public UserInfoDisplayDTO()
        {

        }
    }
}
