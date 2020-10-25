namespace CinelAirMiles.Data.Repositories
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;
    using System.Linq;


    public interface IClientRepository
    {


        IEnumerable<SelectListItem> GetComboGenders();


       

    }
}
