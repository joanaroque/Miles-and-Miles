using CinelAirMilesLibrary.Common.Data.Entities;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using System.Linq;

namespace CinelAirMilesLibrary.Common.Data
{
    public class DataContext : IdentityDbContext<User>
    {

        public DbSet<Country> Countries { get; set; }


        public DbSet<City> Cities { get; set; }


        public DbSet<Advertising> Advertisings { get; set; }


        public DbSet<ClientComplaint> ClientComplaints { get; set; }


        public DbSet<Partner> Partners { get; set; }


        public DbSet<PremiumOffer> PremiumOffers { get; set; }


        public DbSet<TierChange> TierChanges { get; set; }


        public DbSet<PremiumOfferType> PremiumOfferTypes { get; set; }


        public DbSet<Transaction> Transactions { get; set; }


        public DbSet<Reservation> Reservations { get; set; }


        public DbSet<Notification> Notifications { get; set; }



        public DbSet<Flight> Flights { get; set; }



        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>(entity =>
            {
                entity.Property<string>("CreatedById");
                entity.Property<string>("ModifiedById");

                entity.HasOne("CinelAirMilesLibrary.Common.Data.Entities.User", "CreatedBy")
                .WithOne()
                .HasForeignKey("User", "CreatedById");


                entity.HasOne("CinelAirMilesLibrary.Common.Data.Entities.User", "ModifiedBy")
                .WithOne()
                .HasForeignKey("User", "ModifiedById");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.Property<string>("CreatedById");
                entity.Property<string>("ModifiedById");

                entity.HasOne("CinelAirMilesLibrary.Common.Data.Entities.User", "CreatedBy")
                .WithOne()
                .HasForeignKey("User", "CreatedById"); ;

                entity.HasOne("CinelAirMilesLibrary.Common.Data.Entities.User", "ModifiedBy")
                .WithOne()
                .HasForeignKey("User", "ModifiedById");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasOne("CinelAirMilesLibrary.Common.Data.Entities.City", "City")
                .WithMany();

                entity.HasOne("CinelAirMilesLibrary.Common.Data.Entities.Country", "Country")
                .WithMany();
            });


            modelBuilder.Entity<Transaction>()
              .Property(p => p.Price)
              .HasColumnType("decimal(18,2)");




            var cascadeFKs = modelBuilder.Model
                .GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys()
                .Where(fk => fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade));

            foreach (var fk in cascadeFKs)
            {
                fk.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(modelBuilder);

        }

    }
}
