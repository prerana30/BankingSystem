using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;

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

        /*public KycDocument(int KYCId, int userId, string fatherName,
                           string motherName, string grandFatherName,
                           IFormFile UserImageFile, IFormFile CitizenshipImageFile,
                           string permanentAddress)
        {
            KYCId = KYCId;
            UserId = userId;
            FatherName = fatherName;
            MotherName = motherName;
            GrandFatherName = grandFatherName;
            UserImageFile = UserImageFile;
            CitizenshipImageFile = CitizenshipImageFile;
            PermanentAddress = permanentAddress;
        }*/

        public KycDocument() { }
    }
}
