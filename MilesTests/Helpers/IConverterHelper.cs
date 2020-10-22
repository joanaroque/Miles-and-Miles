namespace MilesBackOffice.Web.Helpers
{
    using MilesBackOffice.Web.Data.Entities;
    using MilesBackOffice.Web.Models;
    using MilesBackOffice.Web.Models.SuperUser;
    using MilesBackOffice.Web.Models.User;

    using System;
    using System.Threading.Tasks;

    public interface IConverterHelper
    {

        Advertising ToAdvertising(AdvertisingViewModel model, Guid imageId, bool isNew);


        AdvertisingViewModel ToAdvertisingViewModel(Advertising advertising);


        ClientComplaint ToClientComplaint(ComplaintClientViewModel model, bool isNew);


        ComplaintClientViewModel ToComplaintClientViewModel(ClientComplaint clientComplaint);


        //SeatsAvailable ToSeatsAvailable(AvailableSeatsViewModel model, bool isNew);


        //AvailableSeatsViewModel ToAvailableSeatsViewModel(SeatsAvailable seatsAvailable);


        TierChange ToTierChange(TierChangeViewModel model, bool isNew);


        TierChangeViewModel ToTierChangeViewModel(TierChange tierChange);


        Partner ToPartnerModel(PartnerViewModel model, bool isNew);


        PartnerViewModel ToPartnerViewModel(Partner model);


        Advertising ToNewsModel(AdvertisingViewModel model);


        PremiumOffer ToPremiumOfferModel(PremiumOfferViewModel model, bool isNew, Partner partner);


        PremiumOfferViewModel ToPremiumOfferViewModel(PremiumOffer model);
    }
}
