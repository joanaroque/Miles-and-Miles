using System.Threading.Tasks;

namespace CinelAirMilesLibrary.Common.Hub.Notification
{
    public interface INotify
    {
        Task DbChangeNotification();
    }
}
