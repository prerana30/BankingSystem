using BankingSystem.API.DTO;
using BankingSystem.API.Models;
using BankingSystem.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace BankingSystem.API.Controllers
{
    [ApiController]
    [Route("api/kycdocument")]
    public class KycDocumentController : ControllerBase
    {
        private readonly KycService _kycService;

        public KycDocumentController(KycService kycService)
        {
            _kycService = kycService ?? throw new ArgumentNullException(nameof(kycService));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<KycDocument>>> GetKycDocument()
        {
            var KycDocument = await _kycService.GetKycDocumentAsync();
            return Ok(KycDocument);
        }

        [HttpGet("{KYCId}")]
        public async Task<ActionResult<KycDocument>> GetKycDocument(Guid KYCId)
        {
            var kycDocument = await _kycService.GetKycDocumentAsync(KYCId);
            if (kycDocument == null)
            {
                return NotFound();
            }
            return Ok(kycDocument);
        }

        [HttpGet("{UserId}")]
        public async Task<ActionResult<KycDocument>> GetKycDocumentByUserId(Guid UserId)
        {
            var kycDocument = await _kycService.GetKycByUserIdAsync(UserId);
            if (kycDocument == null)
            {
                return NotFound();
            }
            return Ok(kycDocument);
        }

        [HttpPost]
        public async Task<ActionResult<KycDocument>> AddKycDocumentAsync(KycDocumentDTO kycDocumentDto)
        {
            var kycDocument = await _kycService.AddKycDocumentAsync(kycDocumentDto);
            return Ok(kycDocument);
        }

        [HttpPut("{KYCId}")]
        public async Task<ActionResult<KycDocument>> UpdateKycDocument(Guid KYCId, KycDocumentDTO kycDocumentDto)
        {
            var updatedKycDocument = await _kycService.UpdateKycDocumentAsync(KYCId, kycDocumentDto);
            if (updatedKycDocument == null)
            {
                return BadRequest("Update failed");
            }
            return Ok(updatedKycDocument);
        }
    }
}
