﻿namespace MilesBackOffice.Web.Helpers
{
    using System;

    using CinelAirMilesLibrary.Common.Data.Entities;

    using MilesBackOffice.Web.Models;
    using MilesBackOffice.Web.Models.Admin;
    using MilesBackOffice.Web.Models.SuperUser;

    public interface IConverterHelper
    {
        Advertising ToAdvertising(AdvertisingViewModel model, bool isNew, string path, Partner partner);



        AdvertisingViewModel ToAdvertisingViewModel(Advertising advertising);



        Flight ToFlight(FlightViewModel model, bool isNew);



        FlightViewModel ToFlightViewModel(Flight advertising);



        ClientComplaint ToClientComplaint(ComplaintClientViewModel model, bool isNew);



        ComplaintClientViewModel ToComplaintClientViewModel(ClientComplaint clientComplaint);



        TierChange ToTierChange(TierChangeViewModel model, bool isNew);



        TierChangeViewModel ToTierChangeViewModel(TierChange tierChange);



        Partner ToPartnerModel(PartnerViewModel model, bool isNew);



        PartnerViewModel ToPartnerViewModel(Partner model);



        PremiumOffer ToPremiumOfferModel(PremiumOfferViewModel model, bool isNew, Partner partner, Flight flight);



        PremiumOfferViewModel ToPremiumOfferViewModel(PremiumOffer model);


        UserDetailsViewModel ToUserViewModel(User user);


        User ToUser(UserDetailsViewModel model);
    }
}
