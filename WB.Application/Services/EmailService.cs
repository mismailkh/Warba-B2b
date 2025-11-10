using Microsoft.Extensions.Configuration;
using MailKit.Security;
using MimeKit.Text;
using MimeKit;
using MailKit.Net.Smtp;
using WB.Application.Interfaces.Services;
using WB.Shared.Dtos.General.RequestDtos;

namespace WB.Application.Services
{
    public class EmailService(IConfiguration config) : IEmailService
    {

        public async Task<Tuple<bool, Exception?>> SendEmail(EmailRequestDto request)
        {
            try
            {
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(config.GetSection("EmailSettings:Username").Value));
                email.To.Add(MailboxAddress.Parse(request.To));

                email.Subject = request.Subject;
                email.Body = new TextPart(TextFormat.Html) { Text = request.Message };

                using var smtp = new SmtpClient();
                await smtp.ConnectAsync(config.GetSection("EmailSettings:Host").Value, Convert.ToInt32(config.GetSection("EmailSettings:Port").Value), SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(config.GetSection("EmailSettings:Username").Value, config.GetSection("EmailSettings:Password").Value);
                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);
                return new Tuple<bool, Exception?>(false, null);
            }
            catch (Exception ex)
            {
                return new Tuple<bool, Exception?>(false, ex);
            }

        }
    }
}