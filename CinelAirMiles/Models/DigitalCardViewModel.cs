namespace CinelAirMiles.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using CinelAirMilesLibrary.Common.Enums;


    public class DigitalCardViewModel
    {

        [Display(Name = "Name: ")]
        public string Name { get; set; }



        [Display(Name = "Client Nrº: ")]
        public string ClientNumber { get; set; }


        [Display(Name = "Tier: ")]
        public TierType TierType { get; set; }



        [Display(Name = "Expiration: ")]
        [Required(ErrorMessage = "Must insert the {0}")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ExpirationDate { get; set; }



        [Display(Name = "Image")]
        public string ImageUrl { get; set; }


        [Display(Name = "Image")]
        public string ImageFullPath
        {
            get
            {
                if (string.IsNullOrEmpty(ImageUrl))
                {
                    return null;
                }
                return $"https://cinelairmiles.azurewebsites.net{ImageUrl.Substring(1)}";
            }
        }
    }
}
