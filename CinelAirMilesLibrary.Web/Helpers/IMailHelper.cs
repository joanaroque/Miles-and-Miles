namespace CinelAirMilesLibrary.Common.Helpers
{
    public interface IMailHelper
    {
        /// <summary>
        /// admin send approval account email
        /// </summary>
        /// <param name="to"></param>
        /// <param name="toName"></param>
        void SendApproveClient(string to, string toName, string userId);

        /// <summary>
        /// send email with paramenters
        /// </summary>
        /// <param name="to"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        void SendMail(string to, string subject, string body);

        /// <summary>
        /// confirmation email after register
        /// </summary>
        /// <param name="to"></param>
        /// <param name="token"></param>
        /// <param name="toName"></param>
        void SendToNewClient(string to, string token, string toName);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="to"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="pdf"></param>
        void SendMailWithAttachment(string to, string subject, string body, byte[] pdf);


        /// <summary>
        /// confirm subscrive email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        bool SendNewsletterConfirmation(string email);

        /// <summary>
        /// admin sends refuse account email
        /// </summary>
        /// <param name="to"></param>
        /// <param name="toName"></param>
        void SendRefuseClient(string to, string toName);

        /// <summary>
        /// admin sends a new User (backoffice) email with a reset password token
        /// </summary>
        /// <param name="to"></param>
        /// <param name="token"></param>
        /// <param name="toName"></param>
        void SendToNewUser(string to, string token, string toName);

        /// <summary>
        /// andimnistrator sends an email to reset password of a user
        /// </summary>
        /// <param name="email"></param>
        /// <param name="name"></param>
        /// <param name="link"></param>
        void SendResetEmail(string email, string name, string link);
    }
}
