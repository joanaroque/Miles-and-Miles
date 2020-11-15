namespace MilesBackOffice.Web.Helpers
{
    using System;

    using CinelAirMilesLibrary.Common.Data.Entities;

    using MilesBackOffice.Web.Models;
    using MilesBackOffice.Web.Models.Admin;
    using MilesBackOffice.Web.Models.SuperUser;

    public interface IConverterHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="isNew"></param>
        /// <param name="path"></param>
        /// <param name="partner"></param>
        /// <returns></returns>
        Advertising ToAdvertising(AdvertisingViewModel model, bool isNew, Partner partner);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="advertising"></param>
        /// <returns></returns>
        AdvertisingViewModel ToAdvertisingViewModel(Advertising advertising);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="isNew"></param>
        /// <returns></returns>
        Flight ToFlight(FlightViewModel model, bool isNew);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="advertising"></param>
        /// <returns></returns>
        FlightViewModel ToFlightViewModel(Flight advertising);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="isNew"></param>
        /// <returns></returns>
        ClientComplaint ToClientComplaint(ComplaintClientViewModel model, bool isNew);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientComplaint"></param>
        /// <returns></returns>
        ComplaintClientViewModel ToComplaintClientViewModel(ClientComplaint clientComplaint);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="isNew"></param>
        /// <returns></returns>
        TierChange ToTierChange(TierChangeViewModel model, bool isNew);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="tierChange"></param>
        /// <returns></returns>
        TierChangeViewModel ToTierChangeViewModel(TierChange tierChange);

        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="isNew"></param>
        /// <returns></returns>
        Partner ToPartnerModel(PartnerViewModel model, bool isNew);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        PartnerViewModel ToPartnerViewModel(Partner model);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="isNew"></param>
        /// <param name="partner"></param>
        /// <param name="flight"></param>
        /// <returns></returns>
        PremiumOffer ToPremiumOfferModel(PremiumOfferViewModel model, bool isNew, Partner partner, Flight flight);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        PremiumOfferViewModel ToPremiumOfferViewModel(PremiumOffer model);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        UserDetailsViewModel ToUserViewModel(User user);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        User ToUser(RegisterUserViewModel model, Country country);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        NewClientsViewModel ToNewClientViewModel(User user);
        NotifyAdminViewModel ToNotifyViewModel(User user);
    }
}
