using System;
using System.ComponentModel.DataAnnotations;

namespace MilesBackOffice.Web.Models.SuperUser
{
    public class AdvertisingViewModel
    {
        public string Id { get; set; }



        public string Title { get; set; }



        public string Content { get; set; }



        [Display(Name = "Image")]
        public Guid ImageId { get; set; }


        [Required(ErrorMessage = "Must insert the {0}")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }



        public string PartnerId { get; set; }



        public bool IsConfirm { get; set; }
    }
}
