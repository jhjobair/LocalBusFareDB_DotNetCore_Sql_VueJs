using MailKit.Net.Smtp;
using Microsoft.Extensions.Options; // Required for IOptions
using MimeKit;
using WebApi.Interface;
using WebApi.Models.jwtToken;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace WebApi.Services
{
    // We inject IOptions<SmtpSettings>
    public class EmailService(IOptions<SmtpSettings> smtpSettings, IConfiguration config) : IEmailService
    {
        private readonly SmtpSettings _settings = smtpSettings.Value;

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            var rawEmail = config.GetValue<string>("SmtpSettings:Email");

            if (string.IsNullOrEmpty(rawEmail))
            {
                // If this hits, C# cannot find the "SmtpSettings" section at all.
                throw new Exception("CRITICAL: C# cannot find SmtpSettings in ANY appsettings file.");
            }

            var email = new MimeMessage();
            // Use values from _settings
            email.From.Add(MailboxAddress.Parse(_settings.Email));
            email.To.Add(MailboxAddress.Parse(toEmail));
            email.Subject = subject;
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = body };

            using var smtp = new SmtpClient();

            // Use Host and Port from appsettings
            await smtp.ConnectAsync(_settings.Host, _settings.Port, MailKit.Security.SecureSocketOptions.StartTls);

            // Use Email and Password from appsettings
            await smtp.AuthenticateAsync(_settings.Email, _settings.Password);

            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
    }
}