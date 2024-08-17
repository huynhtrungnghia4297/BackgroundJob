using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Options;
using BackgroundJob;

public class EmailService
{
    private readonly MailSetting _mailSettings;

    public EmailService (IOptions<MailSetting> mailSetting)
    {
        _mailSettings = mailSetting.Value;
    }

    public void SendMail(string toEmail, string subject, string body)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("Your App", _mailSettings.FromEmail));
        message.To.Add(new MailboxAddress("", toEmail));
        message.Subject = subject;

        message.Body = new TextPart("plain")
        {
            Text = body
        };


        //Vòng lặp sendmail cho list users
        //Em đã test bằng email và password email của em đã setup trong appsettungs.json và đã gửi maill thành công.   
        //Tạm thời em rem lại 4 rows bên dưới vì em đã đổi lại mật khẩu gmail.
       
        using (var client = new SmtpClient())
        {
            
            //client.Connect(_mailSettings.SmtpServer, _mailSettings.SmtpPort, MailKit.Security.SecureSocketOptions.StartTls);
            //client.Authenticate(_mailSettings.SmtpUser, _mailSettings.SmtpPass);
            //client.Send(message);
            //client.Disconnect(true);
        }
    }
}

