using Microsoft.Extensions.Options;
using MimeKit;
using ShareIt.Core.Application;
using ShareIt.Core.Application.Interfaces.Infrastructure;
using ShareIt.Core.Domain;
using System.Net.Mail;
using MailKit.Net.Smtp;
using MailKit.Security;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace ShareIt.Infrastructure.Shared.Services
{

    public class EmailServices : IEmailServices
    {
        public EmailSettings _mailSettings { get; }

        public EmailServices(IOptions<EmailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }
        public async Task SendAsync(EmailRequest request)
        {
            try
            {

                var email = new MimeMessage();
                email.Sender = MailboxAddress.Parse(request.From ?? _mailSettings.EmailFrom);
                email.To.Add(MailboxAddress.Parse(request.To));
                email.Subject = request.Subject;
                var builder = new BodyBuilder();
                builder.HtmlBody = request.Body;
                email.Body = builder.ToMessageBody();
                using var smtp = new SmtpClient();
                smtp.ServerCertificateValidationCallback = (s, c, h, e) => true;
                smtp.Connect(_mailSettings.SmtpHost, _mailSettings.SmtpPort, SecureSocketOptions.StartTls);
                smtp.Authenticate(_mailSettings.SmtpUser, _mailSettings.SmtpPass);
                await smtp.SendAsync(email);
                smtp.Disconnect(true);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
