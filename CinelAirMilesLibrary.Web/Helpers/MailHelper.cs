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
                        $"            <h1 style = 'color: #e67e22; margin: 0 0 7px' > Hello & Welcome to Miles Program Team </h1>" +
                        $"      </ul>" +
                        $"  <div style = 'width: 100%;margin:20px 0; display: inline-block;text-align: center'>" +
                        "       <h4 style = 'color: #e67e22; margin: 0 0 7px' >After confirming your email you'll be able to access our platform. </h4>"+
                        "       <h4 style = 'color: #e67e22; margin: 0 0 7px' >Let's fly together. </h4>" +
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


        public void SendToNewClient(string to, string token, string toName)
        {
            var config = GetMailConfig();

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(config.NameFrom, config.From));
            message.To.Add(new MailboxAddress(toName, to));

            message.Subject = "Welcome to Miles Program";

            var bodybuilder = new BodyBuilder
            {
                HtmlBody = $" <td style = 'background-color: #ecf0f1'>" +
                        $"      <div style = 'color: #34495e; margin: 4% 10% 2%; text-align: justify;font-family: sans-serif'>" +
                        $"            <h1 style = 'color: #e67e22; margin: 0 0 7px' > Hello & Welcome to the CinelAir Miles Program Family </h1>" +
                        $"      </ul>" +
                        $"  <div style = 'width: 100%;margin:20px 0; display: inline-block;text-align: center'>" +
                        $"  </div>" +
                        $"  <div style = 'width: 100%; text-align: center'>" +
                        $"    <h2 style = 'color: #e67e22; margin: 0 0 7px' >Email Confirmation </h2>" +
                        $"    We need to confirm your email address, please click the link:</br></br> " +
                        $"    <a style ='text-decoration: none; border-radius: 2px; padding: 5px 15px; color: white; background-color: #3498db' href = \"{token}\">Complete Registration</a>" +
                        $"    <p style = 'color: #b3b3b3; font-size: 12px; text-align: center;margin: 30px 0 0' > Your account will be approved soon after. </p>" +
                        $"    <p style = 'color: #b3b3b3; font-size: 12px; text-align: center;margin: 30px 0 0' >We'll let you know when it's approved and ready for you to login and start gaining Miles. </p>" +
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





        public void SendApproveClient(string to, string toName, string userId)
        {
            var config = GetMailConfig();

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(config.NameFrom, config.From));
            message.To.Add(new MailboxAddress(toName, to));

            message.Subject = "Welcome to Miles Program";

            var bodybuilder = new BodyBuilder
            {
                HtmlBody = "<table> <tr> " +
                        "   <td style = 'background-color: #ecf0f1'>" +
                        "       <div style = 'color: #34495e; margin: 10% 10; text-align: justify;font-family: sans-serif'>" +
                        "           <h1 style = 'color: #e67e22; margin: 5px' > Welcome to Miles Program </h1>" +
                        "           <div style = 'width: 100%;margin:20px 0; display: inline-block;text-align: center'>" +
                        "           <h3>Your account has been approved!</h3>" +
                        "       </div>" +
                         "       <div style = 'width: 100%; text-align: center'>" +
                        $"           <h5 style = 'color: #e67e22; margin: 0 0 7px'> Your Miles Program ID: {userId} </h5>" +
                        "           <h5 style = 'color: #e67e22; margin: 0 0 7px'> You will need this Id to login in our applications. Don't lose it!</h5>" +
                        "       </div>" +
                        "       <div style = 'width: 100%; text-align: center'>" +
                        "           <h5 style = 'color: #e67e22; margin: 0 0 7px'> You can start earning Miles with us and our Partners! </h5>" +
                        "           <h5 style = 'color: #e67e22; margin: 0 0 7px'> Thank you for choosing Cinel Air.</h5>" +
                        "           </br></br> " +
                        "           <p style = 'color: #b3b3b3; font-size: 12px; text-align: center;margin: 30px 0 0' > CinelAir Miles Program 2020</p>" +
                        "       </div>" +
                        "   </td >" +
                        "   </tr>" +
                        "</table>"
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


        public void SendRefuseClient(string to, string toName)
        {
            var config = GetMailConfig();

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(config.NameFrom, config.From));
            message.To.Add(new MailboxAddress(toName, to));

            message.Subject = "Welcome to Miles Program Team";

            var bodybuilder = new BodyBuilder
            {
                HtmlBody = "<table> <tr> " +
                        "   <td style = 'background-color: #ecf0f1'>" +
                        "       <div style = 'color: #34495e; margin: 10% 10; text-align: justify;font-family: sans-serif'>" +
                        "           <h1 style = 'color: #e67e22; margin: 5px' > Miles Program </h1>" +
                        "           <div style = 'width: 100%;margin:20px 0; display: inline-block;text-align: center'>" +
                        "           <h3>Your account has been refused by an Administrator.</h3>" +
                        "       </div>" +
                        "       <div style = 'width: 100%; text-align: center'>" +
                        "           <h5 style = 'color: #e67e22; margin: 0 0 7px'> Contact us to learn more about your refusal. </h5>" +
                        "           <h5 style = 'color: #e67e22; margin: 0 0 7px'> Thank you for choosing Cinel Air.</h5>" +
                        "           </br></br> " +
                        "           <p style = 'color: #b3b3b3; font-size: 12px; text-align: center;margin: 30px 0 0' > CinelAir Miles Program 2020</p>" +
                        "       </div>" +
                        "   </td >" +
                        "   </tr>" +
                        "</table>"
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


        public bool SendNewsletterConfirmation(string email)
        {
            var config = GetMailConfig();

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(config.NameFrom, config.From));
            message.To.Add(new MailboxAddress(email, email));

            message.Subject = "Welcome to Miles Program Team";

            var bodybuilder = new BodyBuilder
            {
                HtmlBody = "<table> <tr> " +
                        "   <td style = 'background-color: #ecf0f1'>" +
                        "       <div style = 'color: #34495e; margin: 10% 10; text-align: justify;font-family: sans-serif'>" +
                        "           <h1 style = 'color: #e67e22; margin: 5px' > Miles Program </h1>" +
                        "           <div style = 'width: 100%;margin:20px 0; display: inline-block;text-align: center'>" +
                        "           <h3>Your subscription has been activated.</h3>" +
                        "       </div>" +
                        "       <div style = 'width: 100%; text-align: center'>" +
                        "           <h5 style = 'color: #e67e22; margin: 0 0 7px'>We won't spam your email account.</h5>" +
                        "           <h5 style = 'color: #e67e22; margin: 0 0 7px'> Thank you for choosing Cinel Air.</h5>" +
                        "           </br></br> " +
                        "           <p style = 'color: #b3b3b3; font-size: 12px; text-align: center;margin: 30px 0 0' > CinelAir Miles Program 2020</p>" +
                        "       </div>" +
                        "   </td >" +
                        "   </tr>" +
                        "</table>"
            };

            message.Body = bodybuilder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                client.Connect(config.Smtp, int.Parse(config.Port), false);
                client.Authenticate(config.From, config.Password);
                client.Send(message);
                client.Disconnect(true);
            }
            return true;
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
