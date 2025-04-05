using MailKit.Net.Smtp;
using MimeKit;

public class EmailService
{
    private readonly IConfiguration _config;

    public EmailService(IConfiguration config)
    {
        _config = config;
    }

    public void SendEmail(string toEmail, string subject, string body)
    {
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(_config["Email:From"]));
        email.To.Add(MailboxAddress.Parse(toEmail));
        email.Subject = subject;

        var builder = new BodyBuilder { HtmlBody = body };
        email.Body = builder.ToMessageBody();

        using var smtp = new SmtpClient();
        smtp.Connect(_config["Email:SmtpServer"], int.Parse(_config["Email:Port"]), true);
        smtp.Authenticate(_config["Email:Username"], _config["Email:Password"]);
        smtp.Send(email);
        smtp.Disconnect(true);
    }
}
