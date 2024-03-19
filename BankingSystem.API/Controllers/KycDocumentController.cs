using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BankingSystem.API.DTO;
using BankingSystem.API.Models;
using BankingSystem.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
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

        [HttpPost]
        public async Task<ActionResult<KycDocument>> AddKycDocumentAsync([FromBody] KycDocumentDTO kycDocumentDto)
        {
            var kycDocument = await _kycService.AddKycDocumentAsync(kycDocumentDto);
            return Ok(kycDocument);
        }
        

        [HttpDelete("{KYCId}")]
        public async Task<IActionResult> DeleteKycDocument(Guid KYCId)
        {
             _kycService.DeleteKycDocument(KYCId);
            return NoContent();
        }

        [HttpPut("{KYCId}")]
        public async Task<ActionResult<KycDocument>> UpdateKycDocument(Guid KYCId, [FromBody] KycDocumentDTO kycDocumentDto)
        {
            var updatedKycDocument = await _kycService.UpdateKycDocumentAsync(KYCId, kycDocumentDto);
            if (updatedKycDocument == null)
            {
                return BadRequest("Update failed");
            }
            return Ok(updatedKycDocument);
        }

        [HttpPatch("{KYCId}")]
        public async Task<ActionResult<KycDocument>> PatchKycDocument(Guid KYCId, [FromBody] JsonPatchDocument<KycDocumentDTO> patchDocument)
        {
            var patchedKycDocument = await _kycService.UpdateKycDocumentAsync(KYCId, patchDocument);
            if (patchedKycDocument == null)
            {
                return NotFound();
            }
            return Ok(patchedKycDocument);
        }
    }
}
