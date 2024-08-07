namespace Municipal.Application.Legacy.Features.Email;

public class SendEmailRequest
{
    public string ToEmail { get; set; }
    public string Subject { get; set; }
    public string html { get; set; }
    public string From { get; set; } = null;
}