using BankingSystem.API.Models;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using AutoMapper;
using BankingSystem.API.IRepository;




namespace BankingSystem.API.Services
{
    public class EmailService
    {
        private readonly IConfiguration configuration;

        public EmailService(IConfiguration _configuration)
        {
            configuration = _configuration;
        }




        //public EmailService(IConfiguration configuration , )
        //{
        //     Email = configuration["EmailSettings:SenderEmail"];
        //     configuration["EmailSettings:SenderPassword"];
        //}
        public async Task SendEmailAsync(Email email)
        {
            var senderEmail = configuration["EmailSettings:SenderEmail"];
            var senderPassword = configuration["EmailSettings:SenderPassword"];
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(senderEmail,senderPassword),
                EnableSsl = true
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(senderEmail),
                Subject =email.MailSubject,
                Body = email.MailBody,
                IsBodyHtml = true
            };
            mailMessage.To.Add(email.ReceiverEmail);

            try
            {
                await smtpClient.SendMailAsync(mailMessage);
            }
            catch (Exception ex)
            {
                // Handle exception (log, etc.)
                throw ex;
            }
            finally
            {
                // Dispose the smtpClient
                smtpClient.Dispose();
            }
        }
    }
}
