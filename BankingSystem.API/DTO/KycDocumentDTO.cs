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
        public string? UserImagePath { get; set; }

        [Required]
        public string CitizenshipImagePath { get; set; }

        public string PermanentAddress { get; set; }

        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;

        public bool IsApproved { get; set; }

        public KycDocumentDTO(Guid userId, string fatherName,
                                string motherName, string grandFatherName,
                                string userImagePath, string citizenshipImagePath,
                                string permanentAddress)
        {
            UserId = userId;
            FatherName = fatherName;
            MotherName = motherName;
            GrandFatherName = grandFatherName;
            UserImagePath = userImagePath;
            CitizenshipImagePath = citizenshipImagePath;
            PermanentAddress = permanentAddress;
        }

        public KycDocumentDTO()
        {
        }
    }
}
