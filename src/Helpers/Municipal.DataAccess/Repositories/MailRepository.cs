using FluentResults;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using Municipal.Application.Legacy.Abstracts;
using Municipal.Application.Legacy.Features.Email;
using Municipal.Utils.Options;
namespace Municipal.DataAccess.Repositories;

public class MailRepository : IMailRepository
{
    private readonly MailSettings _mailSettings;

    public MailRepository(IOptions<MailSettings> mailSettings)
    {
        _mailSettings = mailSettings.Value;
    }

    public async Task<Result<string>> SendEmailAsync(SendEmailRequest request)
    {
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(request.From ?? _mailSettings.Mail));
        email.To.Add(MailboxAddress.Parse(request.ToEmail));
        email.Subject = request.Subject;
        email.Body = new TextPart(TextFormat.Html) { Text = request.html };

        // send email
        using var smtp = new SmtpClient();
        smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
        smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
        smtp.Send(email);
        smtp.Disconnect(true);
            
        return "تم الارسال";
    }
}