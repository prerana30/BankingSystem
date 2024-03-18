using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BankingSystem.API.DTO
{
    public class KycDocumentDTO
    {
            [Required]
            [ForeignKey("Users")]
            public int UserId { get; set; }

            [Required]
            [MaxLength(50)]
            public string FatherName { get; set; }

            [Required]
            public string MotherName { get; set; }

            [Required]
            [MaxLength(50)]
            public string GrandFatherName { get; set; }

            [Required]
            public IFormFile UserImagePath { get; set; }

            [Required]
            public IFormFile CitizenshipImagePath { get; set; }

            public string PermanentAddress { get; set; }

            public DateTime UploadedAt { get; set; } = DateTime.Now;

            public KycDocumentDTO(int userId, string fatherName,
                               string motherName, string grandFatherName,
                               IFormFile userImagePath, IFormFile citizenshipImagePath,
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
    }
}
