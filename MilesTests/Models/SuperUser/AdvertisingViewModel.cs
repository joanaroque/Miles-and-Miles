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



        public DateTime EndDate { get; set; }



        public string PartnerId { get; set; }



        public bool IsConfirm { get; set; }
    }
}
