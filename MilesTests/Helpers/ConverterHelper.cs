namespace MilesBackOffice.Web.Helpers
{
    using System;

    using CinelAirMilesLibrary.Common.Data.Entities;
    using CinelAirMilesLibrary.Common.Helpers;

    using MilesBackOffice.Web.Models;
    using MilesBackOffice.Web.Models.SuperUser;


    public class ConverterHelper : IConverterHelper
    {
        public ConverterHelper()
        {

        }

        public FlightViewModel ToFlightViewModel(Flight flight)
        {
            var seats = new FlightViewModel
            {
                FlightId = flight.Id,
                MaximumSeats = flight.MaximumSeats,
                AvailableSeats = flight.AvailableSeats,
                Status = flight.Status,
                Departure = flight.Departure,
                Arrival = flight.Arrival,
                DepartureDate = flight.DepartureDate,
                PartnerName = flight.Partner.CompanyName
            };
            return seats;
        }

        public Flight ToFlight(FlightViewModel model, bool isNew)
        {
            var seats = new Flight
            {
                Id = isNew ? 0 : model.FlightId,
                UpdateDate = DateTime.Now,
                MaximumSeats = model.MaximumSeats,
                AvailableSeats = model.AvailableSeats,
                Departure = model.Departure,
                Arrival = model.Arrival,
                DepartureDate = model.DepartureDate,
                Status = 1
            };

            return seats;
        }

        public Advertising ToAdvertising(AdvertisingViewModel model, bool isNew, string path, Partner partner)
        {
            var advertisng = new Advertising
            {
                Id = isNew ? 0 : model.Id,
                Title = model.Title,
                Content = model.Content,
                ImageUrl = path,
                EndDate = model.EndDate,
                UpdateDate = DateTime.Now,
                Status = 1,
                PostGuidId = isNew ? GuidHelper.CreatedGuid() : model.PostGuidId
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
                ImageUrl = advertising.ImageUrl,
                EndDate = advertising.EndDate,
                Status = advertising.Status,
                PartnerName = advertising.Partner.CompanyName,
                PostGuidId = advertising.PostGuidId,
                FlightId = advertising.Flight == null ? 0 : advertising.Flight.Id,
            };
            return advertisings;
        }

        public ClientComplaint ToClientComplaint(ComplaintClientViewModel model, bool isNew)
        {
            var complaint = new ClientComplaint
            {
                Id = isNew ? 0 : model.Id,
                UpdateDate = DateTime.Now,
                Status = 1,
                Complaint = model.Complaint,
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
                Id = clientComplaint.Id,
                Complaint = clientComplaint.Complaint,
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
                OldTier = model.OldTier,
                NewTier = model.NewTier,
                NumberOfFlights = model.NumberOfFlights,
                NumberOfMiles = model.NumberOfMiles,
                Status = 1
            };

            return tier;
        }

        public TierChangeViewModel ToTierChangeViewModel(TierChange tierChange)
        {
            var tierChanges = new TierChangeViewModel
            {
                TierChangeId = tierChange.Id,
                OldTier = tierChange.OldTier,
                NewTier = tierChange.NewTier,
                NumberOfFlights = tierChange.NumberOfFlights,
                NumberOfMiles = tierChange.NumberOfMiles,
                Status = tierChange.Status,
                ClientName = tierChange.Client.Name
            };
            return tierChanges;
        }

        public PremiumOffer ToPremiumOfferModel(PremiumOfferViewModel model, bool isNew, Partner partner, Flight flight)
        {
            return new PremiumOffer
            {
                Id = isNew ? 0 : model.Id,
                Flight = flight,
                Title = model.Title,
                Quantity = model.Quantity,
                Price = model.Price,
                Partner = partner,
                Conditions = string.IsNullOrWhiteSpace(model.Conditions) ? string.Empty : model.Conditions,
                Type = model.Type,
                Status = 1,
                OfferIdGuid = isNew ? GuidHelper.CreatedGuid() : model.OfferGuidId                
            };
        }

        public PremiumOfferViewModel ToPremiumOfferViewModel(PremiumOffer model)
        {
            var offer = new PremiumOfferViewModel
            {
                Id = model.Id,
                Title = model.Title,
               // FlightId = model.Flight == null ? 0 : model.Flight.Id,
                Conditions = string.IsNullOrEmpty(model.Conditions) ? string.Empty : model.Conditions,
                Quantity = model.Quantity,
                Price = model.Price,
                AvailableSeats = model.Flight == null ? -1 : model.Flight.AvailableSeats,
                Type = model.Type,
                PartnerName = model.Partner.CompanyName,
                OfferGuidId = model.OfferIdGuid,
               // Arrival = string.IsNullOrEmpty(model.Flight.Arrival) ? string.Empty : model.Flight.Arrival,
                //Departure = string.IsNullOrEmpty(model.Flight.Departure) ? string.Empty : model.Flight.Departure,
               // FlightDateTime = model.Flight.DepartureDate,
            };

            return offer;
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
                Status = 1,
                PartnerGuidId = isNew ? GuidHelper.CreatedGuid() : model.PartnerGuidId
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
                Description = model.Description,
                Status = model.Status
            };
        }


        public User ToUser(UserDetailsViewModel model)
        {
            return new User
            {

            };
        }


        public UserDetailsViewModel ToUserViewModel(User user)
        {
            return new UserDetailsViewModel
            {
                Id = user.Id,
                Name = user.Name,
                Address = user.Address,
                PhoneNumber = user.PhoneNumber,
                DateOfBirth = user.DateOfBirth,
                Username = user.UserName,
                City = user.City,
                Email = user.Email,
                IsActive = user.IsActive,
                Gender = user.Gender,
                SelectedRole = user.SelectedRole,
                TIN = user.TIN
            };
        }
    }
}
