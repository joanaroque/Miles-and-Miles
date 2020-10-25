namespace CinelAirMiles.Helpers
{
    public interface IMailHelper
    {
        void SendMail(string to, string subject, string body);

        void SendMailWithAttachment(string to, string subject, string body, byte[] pdf);
    }
}
