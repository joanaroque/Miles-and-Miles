using System;

namespace MilesBackOffice.Web.Data.Entities
{
    public interface IEntity
    {
        int Id { get; set; }


        User CreatedBy { get; set; }


        DateTime CreateDate { get; set; }


        DateTime UpdateDate { get; set; }


        User ModifiedBy { get; set; }


        bool IsConfirm { get; set; }
    }
}
