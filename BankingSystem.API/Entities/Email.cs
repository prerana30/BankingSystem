using System.ComponentModel.DataAnnotations;

namespace BankingSystem.API.Entities
{
    public class Email
    {
        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.(com|net|org|gov|np|edu)$", ErrorMessage = "Invalid pattern.")]

        // [DataType(DataType.EmailAddress)]
        public string SenderEmail { get; set; }
        [Required]
        public string SenderPassword { get; set; }
        public string MailSubject { get; set; }
        [Required]
        public string MailBody { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.(com|net|org|gov|np|edu)$", ErrorMessage = "Invalid pattern.")]
        public string ReceiverEmail { get; set; }
    }

}
