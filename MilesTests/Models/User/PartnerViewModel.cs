namespace MilesBackOffice.Web.Models.User
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using MilesBackOffice.Web.Data.Entities;

    public class PartnerViewModel
    {
        public IEnumerable<Partner> Partners { get; set; }


        public CreatePartnerViewModel CreatePartnerViewModel { get; set; }

    }

    public class CreatePartnerViewModel
    {
        [Required]
        [Display(Name = "Company Name")]
        public string Name { get; set; }


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


        [Required]
        public string LogoImage { get; set; }


        [Required]
        public string Description { get; set; }
    }
}
