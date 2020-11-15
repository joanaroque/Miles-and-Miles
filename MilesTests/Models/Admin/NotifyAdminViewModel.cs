using CinelAirMilesLibrary.Common.Enums;

namespace MilesBackOffice.Web.Models.Admin
{
    public class NotifyAdminViewModel : NewClientsViewModel
    {
        public string Notification { get; set; }

        public string Message { get; set; }

        public UserType SelectedRole { get; set; }

    }
}
