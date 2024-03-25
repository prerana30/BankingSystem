using BankingSystem.API.Entities;
using BankingSystem.API.Services.IServices;
using System.ComponentModel;
using System.Net;
using System.Net.Mail;

namespace BankingSystem.API.Services
{
    public class EmailService: IEmailService
    {
        private readonly IConfiguration configuration;

        public EmailService(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        public virtual async Task<string> SendEmailAsync(Email email)
        {
            var senderEmail = configuration["EmailSettings:SenderEmail"];
            var senderPassword = configuration["EmailSettings:SenderPassword"];

            // Check if receiver email is valid
            if (!IsValidEmail(email.ReceiverEmail))
            {
                return "Invalid email address.";
            }

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(senderEmail, senderPassword),
                EnableSsl = true
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(senderEmail),
                Subject = email.MailSubject,
                Body = email.MailBody,
                IsBodyHtml = true
            };
            mailMessage.To.Add(email.ReceiverEmail);

            try
            {
                smtpClient.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);

                await smtpClient.SendMailAsync(mailMessage);
                return "Email sent successfully.";
            }
            catch (SmtpFailedRecipientsException ex)
            {
                string errorMessage = "Failed to deliver message. ";
                for (int i = 0; i < ex.InnerExceptions.Length; i++)
                {
                    SmtpStatusCode status = ex.InnerExceptions[i].StatusCode;
                    if (status == SmtpStatusCode.MailboxBusy || status == SmtpStatusCode.MailboxUnavailable)
                    {
                        errorMessage += "Delivery failed - retrying in 5 seconds.";
                        await Task.Delay(5000); // Retry after 5 seconds
                        smtpClient.Send(mailMessage); // Retry sending
                    }
                    else
                    {
                        errorMessage += $"Failed to deliver message to {ex.InnerExceptions[i].FailedRecipient}.";
                    }
                }
                return errorMessage;
            }
            catch (Exception ex)
            {
                return $"Error sending email: {ex.Message}";
            }
            finally
            {
                // Dispose the smtpClient
                smtpClient.Dispose();
            }
        }

        // Method to validate email address
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        // Send completed callback method
        private void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            // Get the unique identifier for this asynchronous operation.
            // In this case, you may not need the token parameter.
            // String token = (string)e.UserState;

            if (e.Cancelled)
            {
                Console.WriteLine("Send canceled.");
            }
            if (e.Error != null)
            {
                Console.WriteLine($"Error sending email: {e.Error.ToString()}");
            }
            else
            {
                Console.WriteLine("Email sent successfully.");
            }
        }
    }
}

