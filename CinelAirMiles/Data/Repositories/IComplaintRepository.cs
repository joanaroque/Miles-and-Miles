namespace CinelAirMiles.Data.Repositories
{
    using CinelAirMilesLibrary.Common.Data.Entities;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;

    public interface IComplaintRepository : IGenericRepository<ClientComplaint>
    {
        IEnumerable<SelectListItem> GetComboComplaintTypes();
    }
}
