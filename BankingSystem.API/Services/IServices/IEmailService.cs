using BankingSystem.API.Entities;

namespace BankingSystem.API.Services.IServices
{
    public interface IEmailService
    {
        Task<string> SendEmailAsync(Email email);
    }
}
