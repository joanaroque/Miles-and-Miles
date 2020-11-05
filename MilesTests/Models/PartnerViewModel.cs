namespace MilesBackOffice.Web.Models
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    public class PartnerViewModel
    {
        public int Id { get; set; }


        [Required]
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }


        [Required]
        public string Address { get; set; }


        [Required]
        [Display(Name = "Company's URL")]
        public string Url { get; set; }


        /// <summary>
        /// Area of Work
        /// can be a set number of areas of work
        /// </summary>
        /// TODO maybe transform into a class with areas of work ex:
        /// hotels, souvenir shops, transport companies
        [Required]
        public string Designation { get; set; }



        public IFormFile Logo { get; set; }


        [Required]
        public string Description { get; set; }


        public int Status { get; set; }
    }
}
