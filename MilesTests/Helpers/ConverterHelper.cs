namespace MilesBackOffice.Web.Helpers
{
    using MilesBackOffice.Web.Data.Entities;
    using MilesBackOffice.Web.Enums;
    using MilesBackOffice.Web.Models.SuperUser;
    using MilesBackOffice.Web.Models.User;

    using System;
    using System.Threading.Tasks;


    public class ConverterHelper : IConverterHelper
    {
        public ConverterHelper()
        {

        }

        public Advertising ToAdvertising(AdvertisingViewModel model, Guid imageId, bool isNew)
        {
            var advertisng = new Advertising
            {
                Id = isNew ? 0 : model.AdvertisingId,
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
                AdvertisingId = advertising.Id,
                Title = advertising.Title,
                Content = advertising.Content,
                ImageId = advertising.ImageId,
                EndDate = advertising.EndDate,
                Status = advertising.Status

            };
            return advertisings;
        }

        public AvailableSeatsViewModel ToAvailableSeatsViewModel(SeatsAvailable seatsAvailable)
        {
            var seats = new AvailableSeatsViewModel
            {
                FlightId = seatsAvailable.Id,
                MaximumSeats = seatsAvailable.MaximumSeats,
                FlightNumber = seatsAvailable.FlightNumber,
                AvailableSeats = seatsAvailable.AvailableSeats,
                Status = seatsAvailable.Status
            };
            return seats;
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
                Subject = model.Subject,
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
                Subject = clientComplaint.Subject,
                Reply = clientComplaint.Reply,
                Status = clientComplaint.Status
            };

            return complaint;
        }

        public SeatsAvailable ToSeatsAvailable(AvailableSeatsViewModel model, bool isNew)
        {
            var seats = new SeatsAvailable
            {
                Id = isNew ? 0 : model.FlightId,
                UpdateDate = DateTime.Now,
                FlightNumber = model.FlightNumber,
                MaximumSeats = model.MaximumSeats,
                AvailableSeats = model.AvailableSeats,
                Status = 1
            };

            return seats;
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
                Client = tierChange.Client,
                TierChangeId = tierChange.Id,
                OldTier = tierChange.OldTier,
                NewTier = tierChange.NewTier,
                NumberOfFlights = tierChange.NumberOfFlights,
                NumberOfMiles = tierChange.NumberOfMiles,
                Status = tierChange.Status
            };
            return tierChanges;
        }


        public PremiumOffer ToPremiumTicket(CreateTicketViewModel model)
        {
            return new PremiumOffer
            {
                Flight = model.FlightId.ToString(),
                Title = model.Title,
                Quantity = model.Quantity,
                Price = model.Price,
                Type = PremiumType.Ticket,
                Status = 1
            };
        }


        public PremiumOffer ToPremiumUpgrade(CreateUpgradeViewModel model)
        {
            return new PremiumOffer
            {
                Flight = model.FlightId.ToString(),
                Title = model.Title,
                Quantity = model.Quantity,
                Price = model.Price,
                Type = PremiumType.Upgrade,
                Status = 1
            };
        }


        public PremiumOffer ToPremiumVoucher(CreateVoucherViewModel model)
        {
            return new PremiumOffer
            {
                Title = model.Title,
                Quantity = model.Quantity,
                Price = model.Price,
                Type = PremiumType.Voucher,
                Conditions = model.Conditions,
                Status = 1
            };
        }


        public async Task UpdateOfferAsync(PremiumOffer current, PremiumOffer edit)
        {
            await Task.Run(() =>
            {
                current.Flight = string.IsNullOrEmpty(edit.Flight) ? string.Empty : edit.Flight;
                current.Conditions = string.IsNullOrEmpty(edit.Conditions) ? string.Empty : edit.Conditions;
                current.Partner = edit.Partner;
                current.Quantity = edit.Quantity;
                current.Price = edit.Price;
                current.Status = 1;
            });

        }


        public Partner ToPartnerModel(CreatePartnerViewModel model)
        {
            return new Partner
            {
                CompanyName = model.Name,
                Address = model.Address,
                Url = model.Url,
                Designation = model.Designation,
                Description = model.Description,
                Status = 1
            };
        }


        public async Task UpdatePartnerAsync(Partner current, Partner edit)
        {
            await Task.Run(() =>
            {
                current.CompanyName = edit.CompanyName;
                current.Address = edit.Address;
                current.Description = edit.Description;
                current.Designation = edit.Designation;
                current.Logo = edit.Logo;
                current.Url = edit.Url;
                current.Status = 1;
            });
        }


        public News ToNewsModel(PublishNewsViewModel model)
        {
            return new News
            {
                Title = model.Title,
                Body = model.Body,
                Status = 1
            };
        }

        public async Task UpdatePostAsync(News current, News edit)
        {
            await Task.Run(() =>
            {
                current.Title = edit.Title;
                current.Body = edit.Body;
                current.Images = edit.Images;
                current.Status = 1;
            });
        }
    }
}
