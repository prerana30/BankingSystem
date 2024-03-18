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
        public int KYCId { get; set; }

        [Required]
        [ForeignKey("Users")]
        public int UserId { get; set; }

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
        public IFormFile UserImagePath { get; set; }
        public string UserImage { get; set; }

        [Required]
        public IFormFile CitizenshipImagePath { get; set; }
        public string CitizenshipImage { get; set; }

        public string PermanentAddress { get; set; }

        public DateTime UploadedAt { get; set; } = DateTime.Now;

        /*public KycDocument(int KYCId, int userId, string fatherName,
                           string motherName, string grandFatherName,
                           IFormFile userImagePath, IFormFile citizenshipImagePath,
                           string permanentAddress)
        {
            KYCId = KYCId;
            UserId = userId;
            FatherName = fatherName;
            MotherName = motherName;
            GrandFatherName = grandFatherName;
            UserImagePath = userImagePath;
            CitizenshipImagePath = citizenshipImagePath;
            PermanentAddress = permanentAddress;
        }*/

        public KycDocument() { }
    }
}
