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


        PremiumOffer ToPremiumTicket(CreateTicketViewModel model);


        PremiumOffer ToPremiumUpgrade(CreateUpgradeViewModel model);


        PremiumOffer ToPremiumVoucher(CreateVoucherViewModel model);


        Task UpdateOfferAsync(PremiumOffer current, PremiumOffer edit);


        Partner ToPartnerModel(CreatePartnerViewModel model);


        Task UpdatePartnerAsync(Partner current, Partner edit);


        News ToNewsModel(PublishNewsViewModel model);


        Task UpdatePostAsync(News current, News edit);
    }
}
