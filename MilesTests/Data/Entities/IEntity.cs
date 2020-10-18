namespace MilesBackOffice.Web.Data.Entities
{
    using System;

    public interface IEntity
    {
        int Id { get; set; }


        User CreatedBy { get; set; }


        DateTime CreateDate { get; set; }


        DateTime UpdateDate { get; set; }


        User ModifiedBy { get; set; }


        /// <summary>
        /// Defines the object current status:
        /// 0 - Approved by a SU
        /// 1 - Waiting approval by a SU
        /// 2 - Returned by a SU for editing
        /// 3 - To be deleted
        /// 4 - Someother state
        /// </summary>
        int Status { get; set; }
    }
}
