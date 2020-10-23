namespace MilesBackOffice.Web.Helpers
{
    using CinelAirMilesLibrary.Common.Data.Entities;

    using MilesBackOffice.Web.Models;
    using MilesBackOffice.Web.Models.SuperUser;
    using MilesBackOffice.Web.Models.User;

    using System;


    public class ConverterHelper : IConverterHelper
    {
        public ConverterHelper()
        {

        }

        public Advertising ToAdvertising(AdvertisingViewModel model, Guid imageId, bool isNew)
        {
            var advertisng = new Advertising
            {
                Id = isNew ? 0 : model.Id,
                Title = model.Title,
                Content = model.Content,
                ImageId = imageId,
                EndDate = model.EndDate,
                UpdateDate = DateTime.Now,
                Status = 1
            };

            return advertisng;
        }

        public AdvertisingViewModel ToAdvertisingViewModel(Advertising advertising)
        {
            var advertisings = new AdvertisingViewModel
            {
                Id = advertising.Id,
                Title = advertising.Title,
                Content = advertising.Content,
                ImageId = advertising.ImageId,
                EndDate = advertising.EndDate,
                Status = advertising.Status,
                PartnerName = advertising.Partner.CompanyName

            };
            return advertisings;
        }


        public ClientComplaint ToClientComplaint(ComplaintClientViewModel model, bool isNew)
        {
            var complaint = new ClientComplaint
            {
                Id = isNew ? 0 : model.ComplaintId,
                UpdateDate = DateTime.Now,
                Status = 1,
                Title = model.Title,
                Email = model.Email,
                Date = model.Date,
                Body = model.Body,
                Reply = model.Reply

            };

            return complaint;
        }

        public ComplaintClientViewModel ToComplaintClientViewModel(ClientComplaint clientComplaint)
        {
            var complaint = new ComplaintClientViewModel
            {
                ComplaintId = clientComplaint.Id,
                Title = clientComplaint.Title,
                Email = clientComplaint.Email,
                Date = clientComplaint.Date,
                Body = clientComplaint.Body,
                Reply = clientComplaint.Reply,
                Status = clientComplaint.Status,
                ClientName = clientComplaint.CreatedBy.Name
            };

            return complaint;
        }


        public TierChange ToTierChange(TierChangeViewModel model, bool isNew)
        {
            var tier = new TierChange
            {
                Id = isNew ? 0 : model.TierChangeId,
                UpdateDate = DateTime.Now,
                //OldTier = model.OldTier,
                //NewTier = model.NewTier,
                //NumberOfFlights = model.NumberOfFlights,
                //NumberOfMiles = model.NumberOfMiles,
                Status = 1
            };

            return tier;
        }

        public TierChangeViewModel ToTierChangeViewModel(TierChange tierChange)
        {
            var tierChanges = new TierChangeViewModel
            {
                TierChangeId = tierChange.Id,
                //OldTier = tierChange.OldTier,
                //NewTier = tierChange.NewTier,
                //NumberOfFlights = tierChange.NumberOfFlights,
                //NumberOfMiles = tierChange.NumberOfMiles,
                Status = tierChange.Status,
                ClientName = tierChange.Client.Name
            };
            return tierChanges;
        }


        public PremiumOffer ToPremiumOfferModel(PremiumOfferViewModel model, bool isNew, Partner partner)
        {
            return new PremiumOffer
            {
                Id = isNew ? 0 : model.Id,
                Flight = model.FlightId,
                Title = model.Title,
                Quantity = model.Quantity,
                Price = model.Price,
                Partner = partner,
                Conditions = string.IsNullOrWhiteSpace(model.Conditions) ? string.Empty : model.Conditions,
                Type = model.Type,
                Status = 1
            };
        }


        public PremiumOfferViewModel ToPremiumOfferViewModel(PremiumOffer model)
        {
            return new PremiumOfferViewModel
            {
                Id = model.Id,
                Title = model.Title,
                FlightId = string.IsNullOrEmpty(model.Flight) ? string.Empty : model.Flight,
                Conditions = string.IsNullOrEmpty(model.Conditions) ? string.Empty : model.Conditions,
                Quantity = model.Quantity,
                Price = model.Price,
                Type = model.Type
            };
        }

        public Partner ToPartnerModel(PartnerViewModel model, bool isNew)
        {
            return new Partner
            {
                Id = isNew ? 0 : model.Id,
                CompanyName = model.CompanyName,
                Address = model.Address,
                Url = model.Url,
                Designation = model.Designation,
                Description = model.Description,
                Status = 1
            };
        }


        public PartnerViewModel ToPartnerViewModel(Partner model)
        {
            return new PartnerViewModel
            {
                Id = model.Id,
                CompanyName = model.CompanyName,
                Address = model.Address,
                Url = model.Url,
                Designation = model.Designation,
                Description = model.Description
            };
        }



        public Advertising ToNewsModel(AdvertisingViewModel model)
        {
            return new Advertising
            {
                Title = model.Title,
                Content = model.Content,
                EndDate = model.EndDate,
                Status = 1
            };
        }
    }
}
