namespace CinelAirMilesLibrary.Common.Helpers
{
    public interface IMailHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="to"></param>
        /// <param name="toName"></param>
        void SendApproveClient(string to, string toName);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="to"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        void SendMail(string to, string subject, string body);


        void SendToNewClient(string to, string token, string toName);




        /// <summary>
        /// 
        /// </summary>
        /// <param name="to"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="pdf"></param>
        void SendMailWithAttachment(string to, string subject, string body, byte[] pdf);
        bool SendNewsletterConfirmation(string email);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="to"></param>
        /// <param name="toName"></param>
        void SendRefuseClient(string to, string toName);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="to"></param>
        /// <param name="token"></param>
        /// <param name="toName"></param>
        void SendToNewUser(string to, string token, string toName);
    }
}
