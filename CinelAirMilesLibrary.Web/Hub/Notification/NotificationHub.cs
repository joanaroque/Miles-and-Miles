namespace CinelAirMilesLibrary.Common.Hub.Notification
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.SignalR;

    public class NotificationHub : Hub<INotify>
    {
        /*
         * cliente precisa de ser registado num grupo sempre que faz login
         * grupos:
         *  -User
         *  -SuperUser
         *  -Admin
         * individual:
         *  -client
         *  
         *  
         */ 
    }
}
