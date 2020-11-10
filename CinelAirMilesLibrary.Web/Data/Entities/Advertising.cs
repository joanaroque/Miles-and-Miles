namespace CinelAirMilesLibrary.Common.Data.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Advertising : IEntity
    {
        public string Title { get; set; }


        public string Content { get; set; }


        public Flight Flight { get; set; }


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


        [Required(ErrorMessage = "Must insert the {0}")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }



        public Partner Partner { get; set; }


        public string PostGuidId { get; set; }
        /******************************/
        public int Id { get; set; }


        public User CreatedBy { get; set; }


        public DateTime CreateDate { get; set; }


        public DateTime UpdateDate { get; set; }


        public User ModifiedBy { get; set; }


        /// <summary>
        /// 0 - Approved by a SU
        /// 1 - Waiting approval by a SU
        /// 2 - Returned by a SU for editing
        /// 3 - To be deleted
        /// </summary>
        public int Status { get; set; }


    }
}
