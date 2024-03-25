using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BankingSystem.API.Services;
using BankingSystem.API.Models;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Microsoft.Extensions.Configuration;

namespace BankingSystem.Test.UnitTests
{
    public class EmailservicesTest
    {
        [Fact]
        public async Task SendEmailAsync_Sends_Email_Successfully()
        {
            // Arrange
            var email = new Email
            {
                ReceiverEmail = "prerana7717@mbmcsit.edu.np",
                MailSubject = "Your account registered",
                MailBody = "Thank you, your bank account has been registered."
            };

            var configurationMock = new Mock<IConfiguration>();
            configurationMock.Setup(config => config["EmailSettings:SenderEmail"]).Returns("prerana7717@mbmcsit.edu.np");
            configurationMock.Setup(config => config["EmailSettings:SenderPassword"]).Returns("ugkr uglw fbag dmap");

            var emailService = new EmailService(configurationMock.Object);

            // Act
            await emailService.SendEmailAsync(email);

            // Assert
            // Since we can't directly assert that an email was sent, we can consider it successful if no exceptions were thrown
        }
    }
}
