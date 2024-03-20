using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BankingSystem.API.DTO
{
    public class KycDocumentDTO
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        [MaxLength(50)]
        public string FatherName { get; set; }

        [Required]
        public string MotherName { get; set; }

        [Required]
        [MaxLength(50)]
        public string GrandFatherName { get; set; }

        [Required]
        public IFormFile UserImageFile { get; set; }

        [Required]
        public IFormFile CitizenshipImageFile { get; set; }

        [Required]
        public string PermanentAddress { get; set; }

        public KycDocumentDTO()
        {
        }
    }
}
