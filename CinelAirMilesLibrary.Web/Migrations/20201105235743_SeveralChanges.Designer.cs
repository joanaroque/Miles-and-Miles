﻿// <auto-generated />
using System;
using CinelAirMilesLibrary.Common.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MilesBackOffice.Web.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20201105235743_SeveralChanges")]
    partial class SeveralChanges
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CinelAirMilesLibrary.Common.Data.Entities.Advertising", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content");

                    b.Property<DateTime>("CreateDate");

                    b.Property<string>("CreatedById");

                    b.Property<DateTime>("EndDate");

                    b.Property<Guid>("ImageId");

                    b.Property<string>("ModifiedById");

                    b.Property<int?>("PartnerId");

                    b.Property<int>("Status");

                    b.Property<string>("Title");

                    b.Property<DateTime>("UpdateDate");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.HasIndex("ModifiedById");

                    b.HasIndex("PartnerId");

                    b.ToTable("Advertisings");
                });

            modelBuilder.Entity("CinelAirMilesLibrary.Common.Data.Entities.ClientComplaint", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Body");

                    b.Property<int>("Complaint");

                    b.Property<DateTime>("CreateDate");

                    b.Property<string>("CreatedById");

                    b.Property<DateTime>("Date");

                    b.Property<string>("Email");

                    b.Property<string>("ModifiedById");

                    b.Property<string>("Reply");

                    b.Property<int>("Status");

                    b.Property<DateTime>("UpdateDate");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.HasIndex("ModifiedById");

                    b.ToTable("ClientComplaints");
                });

            modelBuilder.Entity("CinelAirMilesLibrary.Common.Data.Entities.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDate");

                    b.Property<string>("CreatedById");

                    b.Property<bool>("IsConfirm");

                    b.Property<string>("ModifiedById");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int>("Status");

                    b.Property<DateTime>("UpdateDate");

                    b.HasKey("Id");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("CinelAirMilesLibrary.Common.Data.Entities.Flight", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AvailableSeats");

                    b.Property<DateTime>("CreateDate");

                    b.Property<string>("CreatedById");

                    b.Property<DateTime>("DepartureDate");

                    b.Property<string>("Destination");

                    b.Property<int>("MaximumSeats");

                    b.Property<int>("Miles");

                    b.Property<string>("ModifiedById");

                    b.Property<string>("Origin");

                    b.Property<int?>("PartnerId");

                    b.Property<int>("Status");

                    b.Property<DateTime>("UpdateDate");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.HasIndex("ModifiedById");

                    b.HasIndex("PartnerId");

                    b.ToTable("Flights");
                });

            modelBuilder.Entity("CinelAirMilesLibrary.Common.Data.Entities.Notification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDate");

                    b.Property<string>("CreatedById");

                    b.Property<string>("ItemId");

                    b.Property<string>("Message");

                    b.Property<string>("ModifiedById");

                    b.Property<int>("Status");

                    b.Property<DateTime>("UpdateDate");

                    b.Property<int>("UserGroup");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.HasIndex("ModifiedById");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("CinelAirMilesLibrary.Common.Data.Entities.Partner", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address");

                    b.Property<string>("CompanyName");

                    b.Property<DateTime>("CreateDate");

                    b.Property<string>("CreatedById");

                    b.Property<string>("Description");

                    b.Property<string>("Designation");

                    b.Property<string>("LogoId");

                    b.Property<string>("ModifiedById");

                    b.Property<int>("Status");

                    b.Property<DateTime>("UpdateDate");

                    b.Property<string>("Url");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.HasIndex("ModifiedById");

                    b.ToTable("Partners");
                });

            modelBuilder.Entity("CinelAirMilesLibrary.Common.Data.Entities.PremiumOffer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Conditions");

                    b.Property<DateTime>("CreateDate");

                    b.Property<string>("CreatedById");

                    b.Property<int?>("FlightId");

                    b.Property<string>("ModifiedById");

                    b.Property<string>("OfferIdGuid");

                    b.Property<int?>("PartnerId");

                    b.Property<int>("Price");

                    b.Property<int>("Quantity");

                    b.Property<int>("Status");

                    b.Property<string>("Title");

                    b.Property<int>("Type");

                    b.Property<DateTime>("UpdateDate");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.HasIndex("FlightId");

                    b.HasIndex("ModifiedById");

                    b.HasIndex("PartnerId");

                    b.ToTable("PremiumOffers");
                });

            modelBuilder.Entity("CinelAirMilesLibrary.Common.Data.Entities.PremiumOfferType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDate");

                    b.Property<string>("CreatedById");

                    b.Property<string>("ModifiedById");

                    b.Property<int>("Status");

                    b.Property<int>("Type");

                    b.Property<DateTime>("UpdateDate");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.HasIndex("ModifiedById");

                    b.ToTable("PremiumOfferTypes");
                });

            modelBuilder.Entity("CinelAirMilesLibrary.Common.Data.Entities.Reservation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDate");

                    b.Property<string>("CreatedById");

                    b.Property<string>("ModifiedById");

                    b.Property<int?>("MyPremiumId");

                    b.Property<string>("ReservationID");

                    b.Property<int>("Status");

                    b.Property<DateTime>("UpdateDate");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.HasIndex("ModifiedById");

                    b.HasIndex("MyPremiumId");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("CinelAirMilesLibrary.Common.Data.Entities.TierChange", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClientId");

                    b.Property<DateTime>("CreateDate");

                    b.Property<string>("CreatedById");

                    b.Property<string>("ModifiedById");

                    b.Property<int>("NewTier");

                    b.Property<int>("NumberOfFlights");

                    b.Property<int>("NumberOfMiles");

                    b.Property<int>("OldTier");

                    b.Property<int>("Status");

                    b.Property<DateTime>("UpdateDate");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("CreatedById");

                    b.HasIndex("ModifiedById");

                    b.ToTable("TierChanges");
                });

            modelBuilder.Entity("CinelAirMilesLibrary.Common.Data.Entities.Transaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDate");

                    b.Property<string>("CreatedById");

                    b.Property<int>("EndBalance");

                    b.Property<string>("ModifiedById");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("ProductId");

                    b.Property<int>("StartBalance");

                    b.Property<int>("Status");

                    b.Property<string>("TransferToId");

                    b.Property<int>("Type");

                    b.Property<DateTime>("UpdateDate");

                    b.Property<int>("Value");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.HasIndex("ModifiedById");

                    b.HasIndex("ProductId");

                    b.HasIndex("TransferToId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("CinelAirMilesLibrary.Common.Data.Entities.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("Address")
                        .HasMaxLength(150);

                    b.Property<int>("BonusMiles");

                    b.Property<string>("City");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<int?>("CountryId");

                    b.Property<int?>("CreatedById");

                    b.Property<DateTime?>("DateOfBirth")
                        .IsRequired();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("Gender")
                        .IsRequired();

                    b.Property<string>("GuidId");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsApproved");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<int?>("ModifiedById");

                    b.Property<string>("Name")
                        .HasMaxLength(100);

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<int>("SelectedRole");

                    b.Property<int>("Status");

                    b.Property<int>("StatusMiles");

                    b.Property<string>("TIN")
                        .IsRequired();

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.HasIndex("CreatedById")
                        .IsUnique()
                        .HasFilter("[CreatedById] IS NOT NULL");

                    b.HasIndex("ModifiedById")
                        .IsUnique()
                        .HasFilter("[ModifiedById] IS NOT NULL");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.HasIndex("UserName")
                        .IsUnique()
                        .HasFilter("[UserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("CinelAirMilesLibrary.Common.Data.Entities.Advertising", b =>
                {
                    b.HasOne("CinelAirMilesLibrary.Common.Data.Entities.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById");

                    b.HasOne("CinelAirMilesLibrary.Common.Data.Entities.User", "ModifiedBy")
                        .WithMany()
                        .HasForeignKey("ModifiedById");

                    b.HasOne("CinelAirMilesLibrary.Common.Data.Entities.Partner", "Partner")
                        .WithMany()
                        .HasForeignKey("PartnerId");
                });

            modelBuilder.Entity("CinelAirMilesLibrary.Common.Data.Entities.ClientComplaint", b =>
                {
                    b.HasOne("CinelAirMilesLibrary.Common.Data.Entities.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById");

                    b.HasOne("CinelAirMilesLibrary.Common.Data.Entities.User", "ModifiedBy")
                        .WithMany()
                        .HasForeignKey("ModifiedById");
                });

            modelBuilder.Entity("CinelAirMilesLibrary.Common.Data.Entities.Flight", b =>
                {
                    b.HasOne("CinelAirMilesLibrary.Common.Data.Entities.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById");

                    b.HasOne("CinelAirMilesLibrary.Common.Data.Entities.User", "ModifiedBy")
                        .WithMany()
                        .HasForeignKey("ModifiedById");

                    b.HasOne("CinelAirMilesLibrary.Common.Data.Entities.Partner", "Partner")
                        .WithMany()
                        .HasForeignKey("PartnerId");
                });

            modelBuilder.Entity("CinelAirMilesLibrary.Common.Data.Entities.Notification", b =>
                {
                    b.HasOne("CinelAirMilesLibrary.Common.Data.Entities.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById");

                    b.HasOne("CinelAirMilesLibrary.Common.Data.Entities.User", "ModifiedBy")
                        .WithMany()
                        .HasForeignKey("ModifiedById");
                });

            modelBuilder.Entity("CinelAirMilesLibrary.Common.Data.Entities.Partner", b =>
                {
                    b.HasOne("CinelAirMilesLibrary.Common.Data.Entities.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById");

                    b.HasOne("CinelAirMilesLibrary.Common.Data.Entities.User", "ModifiedBy")
                        .WithMany()
                        .HasForeignKey("ModifiedById");
                });

            modelBuilder.Entity("CinelAirMilesLibrary.Common.Data.Entities.PremiumOffer", b =>
                {
                    b.HasOne("CinelAirMilesLibrary.Common.Data.Entities.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById");

                    b.HasOne("CinelAirMilesLibrary.Common.Data.Entities.Flight", "Flight")
                        .WithMany()
                        .HasForeignKey("FlightId");

                    b.HasOne("CinelAirMilesLibrary.Common.Data.Entities.User", "ModifiedBy")
                        .WithMany()
                        .HasForeignKey("ModifiedById");

                    b.HasOne("CinelAirMilesLibrary.Common.Data.Entities.Partner", "Partner")
                        .WithMany()
                        .HasForeignKey("PartnerId");
                });

            modelBuilder.Entity("CinelAirMilesLibrary.Common.Data.Entities.PremiumOfferType", b =>
                {
                    b.HasOne("CinelAirMilesLibrary.Common.Data.Entities.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById");

                    b.HasOne("CinelAirMilesLibrary.Common.Data.Entities.User", "ModifiedBy")
                        .WithMany()
                        .HasForeignKey("ModifiedById");
                });

            modelBuilder.Entity("CinelAirMilesLibrary.Common.Data.Entities.Reservation", b =>
                {
                    b.HasOne("CinelAirMilesLibrary.Common.Data.Entities.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById");

                    b.HasOne("CinelAirMilesLibrary.Common.Data.Entities.User", "ModifiedBy")
                        .WithMany()
                        .HasForeignKey("ModifiedById");

                    b.HasOne("CinelAirMilesLibrary.Common.Data.Entities.PremiumOffer", "MyPremium")
                        .WithMany()
                        .HasForeignKey("MyPremiumId");
                });

            modelBuilder.Entity("CinelAirMilesLibrary.Common.Data.Entities.TierChange", b =>
                {
                    b.HasOne("CinelAirMilesLibrary.Common.Data.Entities.User", "Client")
                        .WithMany()
                        .HasForeignKey("ClientId");

                    b.HasOne("CinelAirMilesLibrary.Common.Data.Entities.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById");

                    b.HasOne("CinelAirMilesLibrary.Common.Data.Entities.User", "ModifiedBy")
                        .WithMany()
                        .HasForeignKey("ModifiedById");
                });

            modelBuilder.Entity("CinelAirMilesLibrary.Common.Data.Entities.Transaction", b =>
                {
                    b.HasOne("CinelAirMilesLibrary.Common.Data.Entities.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById");

                    b.HasOne("CinelAirMilesLibrary.Common.Data.Entities.User", "ModifiedBy")
                        .WithMany()
                        .HasForeignKey("ModifiedById");

                    b.HasOne("CinelAirMilesLibrary.Common.Data.Entities.PremiumOffer", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId");

                    b.HasOne("CinelAirMilesLibrary.Common.Data.Entities.User", "TransferTo")
                        .WithMany()
                        .HasForeignKey("TransferToId");
                });

            modelBuilder.Entity("CinelAirMilesLibrary.Common.Data.Entities.User", b =>
                {
                    b.HasOne("CinelAirMilesLibrary.Common.Data.Entities.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId");

                    b.HasOne("CinelAirMilesLibrary.Common.Data.Entities.Country")
                        .WithOne("CreatedBy")
                        .HasForeignKey("CinelAirMilesLibrary.Common.Data.Entities.User", "CreatedById");

                    b.HasOne("CinelAirMilesLibrary.Common.Data.Entities.Country")
                        .WithOne("ModifiedBy")
                        .HasForeignKey("CinelAirMilesLibrary.Common.Data.Entities.User", "ModifiedById");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("CinelAirMilesLibrary.Common.Data.Entities.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("CinelAirMilesLibrary.Common.Data.Entities.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CinelAirMilesLibrary.Common.Data.Entities.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("CinelAirMilesLibrary.Common.Data.Entities.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
