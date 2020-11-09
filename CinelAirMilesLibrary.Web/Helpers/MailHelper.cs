namespace CinelAirMilesLibrary.Common.Helpers
{
    using MailKit.Net.Smtp;

    using Microsoft.Extensions.Configuration;

    using MimeKit;

    public class MailHelper : IMailHelper
    {
        private readonly IConfiguration _configuration;

        public MailHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public void SendMail(string to, string subject, string body)
        {
            var config = GetMailConfig();
            
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(config.NameFrom, config.From));
            message.To.Add(new MailboxAddress(to, to));

            message.Subject = subject;

            var bodybuilder = new BodyBuilder
            {
                HtmlBody = body
            };
            message.Body = bodybuilder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                client.Connect(config.Smtp, int.Parse(config.Port), false);
                client.Authenticate(config.From, config.Password);
                client.Send(message);
                client.Disconnect(true);
            }
        }


        public void SendToNewUser(string to, string token, string toName)
        {
            var config = GetMailConfig();

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(config.NameFrom, config.From));
            message.To.Add(new MailboxAddress(toName, to));

            message.Subject = "Welcome to Miles Program Team";

            var bodybuilder = new BodyBuilder
            {
                HtmlBody = $" <td style = 'background-color: #ecf0f1'>" +
                        $"      <div style = 'color: #34495e; margin: 4% 10% 2%; text-align: justify;font-family: sans-serif'>" +
                        $"            <h1 style = 'color: #e67e22; margin: 0 0 7px' > Hello & Welcome to Miles Program Family </h1>" +
                        $"      </ul>" +
                        $"  <div style = 'width: 100%;margin:20px 0; display: inline-block;text-align: center'>" +
                        $"  </div>" +
                        $"  <div style = 'width: 100%; text-align: center'>" +
                        $"    <h2 style = 'color: #e67e22; margin: 0 0 7px' >Email Confirmation </h2>" +
                        $"    To access our application, please click the link:</br></br> " +
                        $"    <a style ='text-decoration: none; border-radius: 5px; padding: 11px 23px; color: white; background-color: #3498db' href = \"{token}\">Complete Registration</a>" +
                        $"    <p style = 'color: #b3b3b3; font-size: 12px; text-align: center;margin: 30px 0 0' > CinelAir Miles Program 2020</p>" +
                        $"  </div>" +
                        $" </td >" +
                        $"</tr>" +
                        $"</table>"
            };
        
            message.Body = bodybuilder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                client.Connect(config.Smtp, int.Parse(config.Port), false);
                client.Authenticate(config.From, config.Password);
                client.Send(message);
                client.Disconnect(true);
            }
        }

        public void SendMailWithAttachment(string to, string subject, string body, byte[] pdf)
        {
            var nameFrom = _configuration["Mail:NameFrom"];
            var from = _configuration["Mail:From"];
            var smtp = _configuration["Mail:Smtp"];
            var port = _configuration["Mail:Port"];
            var password = _configuration["Mail:Password"];

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(nameFrom, from));
            message.To.Add(new MailboxAddress(to, to));

            message.Subject = subject;

            var bodybuilder = new BodyBuilder
            {
                HtmlBody = body
            };
            bodybuilder.Attachments.Add("order.pdf", pdf);

            message.Body = bodybuilder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                client.Connect(smtp, int.Parse(port), false);
                client.Authenticate(from, password);
                client.Send(message);
                client.Disconnect(true);
            }
        }



        private protected MailConfig GetMailConfig()
        {
            return new MailConfig
            {
                NameFrom = _configuration["Mail:NameFrom"],
                From = _configuration["Mail:From"],
                Smtp = _configuration["Mail:Smtp"],
                Port = _configuration["Mail:Port"],
                Password = _configuration["Mail:Password"]
            };
        }

        private protected class MailConfig
        {
            public string NameFrom { get; set; }

            public string From { get; set; }

            public string Smtp { get; set; }

            public string Port { get; set; }

            public string Password { get; set; }
        }
    }
}
