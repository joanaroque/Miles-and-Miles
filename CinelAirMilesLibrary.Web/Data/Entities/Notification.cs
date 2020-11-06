namespace CinelAirMilesLibrary.Common.Data.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using CinelAirMilesLibrary.Common.Enums;

    public class Notification : IEntity
    {
        /// <summary>
        /// A message to include on the notification.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// The User's role. Used as a filter for specific User Roles.
        /// </summary>
        public UserType UserGroup { get; set; }

        /// <summary>
        /// The internal Id of the item that this notification is created for.
        /// </summary>
        public string ItemId { get; set; }

        /// <summary>
        /// The type associated with the Notification. Used to filter notifications to users.
        /// </summary>
        public NotificationType Type { get; set; }

        /************************************/
        public int Id { get; set; }


        public User CreatedBy { get; set; }

        [Required(ErrorMessage = "Must insert the {0}")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime CreateDate { get; set; }



        public DateTime UpdateDate { get; set; }



        public User ModifiedBy { get; set; }


        /// <summary>
        /// 0 - READ
        /// 1 - NOT READ
        /// </summary>
        public int Status { get; set; }


    }
}
