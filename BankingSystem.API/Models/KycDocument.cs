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
        public Guid Id { get; set; }

        // Navigation property
        [ForeignKey("Id")]
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

        public string UserImagePath { get; set; }

        public string CitizenshipImagePath { get; set; }

        public string PermanentAddress { get; set; }

        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;

        public bool IsApproved { get; set; } = false;

        public KycDocument(Guid kYCId, Guid id, Users user, string fatherName, string motherName, string grandFatherName, string userImagePath, string citizenshipImagePath, string permanentAddress, DateTime uploadedAt, bool isApproved)
        {
            KYCId = kYCId;
            Id = id;
            User = user;
            FatherName = fatherName;
            MotherName = motherName;
            GrandFatherName = grandFatherName;
            UserImagePath = userImagePath;
            CitizenshipImagePath = citizenshipImagePath;
            PermanentAddress = permanentAddress;
            UploadedAt = uploadedAt;
            IsApproved = isApproved;
        }

        public KycDocument() { }
    }
}
