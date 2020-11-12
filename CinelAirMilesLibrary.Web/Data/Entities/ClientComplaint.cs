using System;
using System.ComponentModel.DataAnnotations;

using CinelAirMilesLibrary.Common.Enums;

namespace CinelAirMilesLibrary.Common.Data.Entities
{
    public class ClientComplaint : IEntity
    {

        public ComplaintType Complaint { get; set; }



        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }



        [Required(ErrorMessage = "Must insert the {0}")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }



        public string Body { get; set; }



        public string Reply { get; set; }




        public int Id { get; set; }



        public User CreatedBy { get; set; }


        public DateTime CreateDate { get; set; }


        public DateTime UpdateDate { get; set; }


        public User ModifiedBy { get; set; }

        /// <summary>
        /// 0 - Resolved
        /// 1 - Waiting User Answer
        /// 2 - Waiting Client Answer
        /// </summary>
        public int Status { get; set; }
    }
}
