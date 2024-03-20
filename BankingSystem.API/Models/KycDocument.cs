using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankingSystem.API.Models
{
    public class KycDocument
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid KYCId { get; set; }

        [Required]
        public Guid UserId { get; set; }

        // Navigation property
        [ForeignKey("UserId")]
        public Users User { get; set; }

        [Required]
        [MaxLength(50)]
        public string FatherName { get; set; }

        [Required]
        [MaxLength(50)]
        public string MotherName { get; set; }

        [Required]
        [MaxLength(50)]
        public string GrandFatherName { get; set; }

        [Required]
        public IFormFile UserImageFile { get; set; }

        public string UserImagePath { get; set; }

        [Required]
        public IFormFile CitizenshipImageFile { get; set; }

        public string CitizenshipImagePath { get; set; }

        public string PermanentAddress { get; set; }

        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;

        public bool IsApproved { get; set; } = false;

        public KycDocument(Guid kYCId, Guid userId, Users user, string fatherName, string motherName, string grandFatherName, IFormFile userImageFile, string userImagePath, IFormFile citizenshipImageFile, string citizenshipImagePath, string permanentAddress, DateTime uploadedAt, bool isApproved)
        {
            KYCId = kYCId;
            UserId = userId;
            User = user;
            FatherName = fatherName;
            MotherName = motherName;
            GrandFatherName = grandFatherName;
            UserImageFile = userImageFile;
            UserImagePath = userImagePath;
            CitizenshipImageFile = citizenshipImageFile;
            CitizenshipImagePath = citizenshipImagePath;
            PermanentAddress = permanentAddress;
            UploadedAt = uploadedAt;
            IsApproved = isApproved;
        }

        public KycDocument(Guid kYCId, Guid userId, Users user, string fatherName, string motherName, string grandFatherName, IFormFile userImageFile, IFormFile citizenshipImageFile, string permanentAddress, DateTime uploadedAt, bool isApproved)
        {
            KYCId = kYCId;
            UserId = userId;
            User = user;
            FatherName = fatherName;
            MotherName = motherName;
            GrandFatherName = grandFatherName;
            UserImageFile = userImageFile;
            CitizenshipImageFile = citizenshipImageFile;
            PermanentAddress = permanentAddress;
            UploadedAt = uploadedAt;
            IsApproved = isApproved;
        }

        public KycDocument() { }
    }
}
