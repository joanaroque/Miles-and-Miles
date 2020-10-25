﻿using CinelAirMiles.Models;
using CinelAirMilesLibrary.Common.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinelAirMiles.Data.Repositories
{
    public interface ICountryRepository : IGenericRepository<Country>
    {
        IQueryable GetCountriesWithCities();


        Task<Country> GetCountryWithCitiesAsync(int id);


        Task<City> GetCityAsync(int id);


        Task AddCityAsync(CityViewModel model);


        Task<int> UpdateCityAsync(City city);


        Task<int> DeleteCityAsync(City city);


        IEnumerable<SelectListItem> GetComboCountries();


        IEnumerable<SelectListItem> GetComboCities(int conuntryId);


        Task<Country> GetCountryAsync(City city);
    }
}
