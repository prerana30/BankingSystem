﻿using System.ComponentModel.DataAnnotations;
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

        public string UserImagePath { get; set; }

        public string CitizenshipImagePath { get; set; }

        public string PermanentAddress { get; set; }

        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;

        public bool IsApproved { get; set; } = false;

        public KycDocument() { }
    }
}