﻿using BankingSystem.API.DTOs;
using BankingSystem.API.Entities;
using BankingSystem.API.Services.IServices;
using BankingSystem.API.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace BankingSystem.API.Controllers
{
    /// <summary>
    /// Controller for handling KYC Documents. 
    /// It provides endpoints for retrieving, adding, and updating KYC Documents.
    /// </summary>
    [ApiController]
    [Route("api/kycdocument")]
    public class KycDocumentController : ControllerBase
    {
        private readonly IKycService _kycService;

        /// <summary>
        /// Initializes a new instance of the <see cref="KycDocumentController"/> class.
        /// </summary>
        /// <param name="kycService">The service that handles KYC Documents.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="kycService"/> is null.</exception>
        public KycDocumentController(IKycService kycService)
        {
            _kycService = kycService ?? throw new ArgumentNullException(nameof(kycService));
        }

        /// <summary>
        /// Retrieves all KYC Documents.
        /// </summary>
        /// <returns>A collection of KYC Documents.</returns>
        [HttpGet]
        [CustomAuthorize("TellerPerson")]
        public async Task<ActionResult<IEnumerable<KycDocument>>> GetKycDocument()
        {
            var KycDocument = await _kycService.GetKycDocumentAsync();
            return Ok(KycDocument);
        }

        /// <summary>
        /// Retrieves a KYC Document by its Id.
        /// </summary>
        /// <param name="KycId">The Id of the KYC Document.</param>
        /// <returns>The KYC Document if found, otherwise NotFound.</returns>
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

        /// <summary>
        /// Retrieves a KYC Document by a User Id.
        /// </summary>
        /// <param name="UserId">The Id of the User.</param>
        /// <returns>The KYC Document if found, otherwise NotFound.</returns>
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

        /// <summary>
        /// Adds a new KYC Document.
        /// </summary>
        /// <param name="kycDocumentDto">The DTO containing the data for the new KYC Document.</param>
        /// <returns>The created KYC Document.</returns>
        [HttpPost]
        public async Task<ActionResult<KycDocument>> AddKycDocumentAsync(KycDocumentDTO kycDocumentDto)
        {
            var kycDocument = await _kycService.AddKycDocumentAsync(kycDocumentDto);
            return Ok(kycDocument);
        }

        /// <summary>
        /// Updates a KYC Document.
        /// </summary>
        /// <param name="KycId">The Id of the KYC Document.</param>
        /// <param name="kycDocumentDto">The DTO containing the data for the updated KYC Document.</param>
        /// <returns>The updated KYC Document if successful, otherwise BadRequest.</returns>
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
