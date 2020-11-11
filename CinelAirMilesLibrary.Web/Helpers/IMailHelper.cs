namespace CinelAirMilesLibrary.Common.Helpers
{
    public interface IMailHelper
    {
        void SendApproveClient(string to, string toName);
        void SendMail(string to, string subject, string body);


        void SendMailWithAttachment(string to, string subject, string body, byte[] pdf);
        void SendRefuseClient(string to, string toName);

        void SendToNewUser(string to, string token, string toName);
    }
}
