using BankingSystem.API.DTOs;
using BankingSystem.API.Entities;
using BankingSystem.API.Services;
using BankingSystem.API.Utilities;
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
        [CustomAuthorize("TellerPerson")]
        public async Task<ActionResult<IEnumerable<KycDocument>>> GetKycDocument()
        {
            var KycDocument = await _kycService.GetKycDocumentAsync();
            return Ok(KycDocument);
        }

        [HttpGet("by{KycId}")]
        public async Task<ActionResult<KycDocument>> GetKycDocument(Guid KycId)
        {
            var kycDocument = await _kycService.GetKycDocumentAsync(KycId);
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

        [HttpPut("{KycId}")]
        public async Task<ActionResult<KycDocument>> UpdateKycDocument(Guid KycId, KycDocumentDTO kycDocumentDto)
        {
            var updatedKycDocument = await _kycService.UpdateKycDocumentAsync(KycId, kycDocumentDto);
            if (updatedKycDocument == null)
            {
                return BadRequest("Update failed");
            }
            return Ok(updatedKycDocument);
        }
    }
}
