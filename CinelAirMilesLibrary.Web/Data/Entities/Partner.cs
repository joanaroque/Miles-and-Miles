namespace CinelAirMilesLibrary.Common.Data.Entities
{
    using CinelAirMilesLibrary.Common.Enums;
    using System;

    public class Partner : IEntity
    {
        public string CompanyName { get; set; }


        public string Address { get; set; }


        public string Description { get; set; }


        public PartnerType Designation { get; set; }


        public string Url { get; set; }


        public string LogoId { get; set; }


        public string PartnerGuidId { get; set; }

        /************OBJECT PROPERTIES****************************/


        public int Id { get; set; }


        public User CreatedBy { get; set; }


        public DateTime CreateDate { get; set; }


        public DateTime UpdateDate { get; set; }


        public User ModifiedBy { get; set; }

        /// <summary>
        /// 0 - Approved by a SU
        /// 1 - Waiting approval by a SU
        /// 2 - Returned by a SU for editing
        /// </summary>
        public int Status { get; set; }
    }
}
