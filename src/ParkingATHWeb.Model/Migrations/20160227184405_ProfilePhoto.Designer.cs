using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using ParkingATHWeb.Model;

namespace ParkingATHWeb.Model.Migrations
{
    [DbContext(typeof(ParkingAthContext))]
    [Migration("20160227184405_ProfilePhoto")]
    partial class ProfilePhoto
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ParkingATHWeb.Model.Concrete.GateUsage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateOfUse");

                    b.Property<int>("UserId");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("ParkingATHWeb.Model.Concrete.Message", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BCC");

                    b.Property<string>("CC");

                    b.Property<string>("DisplayFrom");

                    b.Property<string>("From");

                    b.Property<string>("MessageParameters");

                    b.Property<string>("Title");

                    b.Property<string>("To");

                    b.Property<int>("Type");

                    b.Property<int>("UserId");

                    b.Property<long>("ViewInBrowserTokenId");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("ParkingATHWeb.Model.Concrete.Order", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<Guid>("ExtOrderId");

                    b.Property<int>("NumOfCharges");

                    b.Property<int>("OrderPlace");

                    b.Property<int>("OrderState");

                    b.Property<decimal>("Price");

                    b.Property<int>("PriceTresholdId");

                    b.Property<int>("UserId");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("ParkingATHWeb.Model.Concrete.PortalMessage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateDate");

                    b.Property<bool>("IsDisplayed");

                    b.Property<bool>("IsNotification");

                    b.Property<int>("PortalMessageType");

                    b.Property<Guid?>("PreviousMessageId");

                    b.Property<int>("ReceiverUserId");

                    b.Property<string>("Text");

                    b.Property<bool>("ToAdmin");

                    b.Property<int>("UserId");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("ParkingATHWeb.Model.Concrete.PriceTreshold", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("MinCharges");

                    b.Property<decimal>("PricePerCharge");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("ParkingATHWeb.Model.Concrete.Token", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<Guid>("SecureToken");

                    b.Property<int>("TokenType");

                    b.Property<int>("UserId");

                    b.Property<DateTime?>("ValidTo");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("ParkingATHWeb.Model.Concrete.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Charges");

                    b.Property<string>("Email");

                    b.Property<bool>("IsAdmin");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("LastName");

                    b.Property<bool>("LockedOut");

                    b.Property<DateTime?>("LockedTo");

                    b.Property<string>("Name");

                    b.Property<long?>("PasswordChangeTokenId");

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PasswordSalt");

                    b.Property<long?>("SelfDeleteTokenId");

                    b.Property<int>("UnsuccessfulLoginAttempts");

                    b.Property<int>("UserPreferencesId");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("ParkingATHWeb.Model.Concrete.UserPreferences", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<byte[]>("ProfilePhoto");

                    b.Property<bool>("ShrinkedSidebar");

                    b.Property<int>("UserId");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("ParkingATHWeb.Model.Concrete.Weather", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Clouds");

                    b.Property<DateTime>("DateOfRead");

                    b.Property<double>("Humidity");

                    b.Property<double>("Pressure");

                    b.Property<double>("Temperature");

                    b.Property<DateTime>("ValidToDate");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("ParkingATHWeb.Model.Concrete.WeatherInfo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("WeatherConditionId");

                    b.Property<string>("WeatherDescription");

                    b.Property<Guid>("WeatherId");

                    b.Property<string>("WeatherMain");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("ParkingATHWeb.Model.Concrete.GateUsage", b =>
                {
                    b.HasOne("ParkingATHWeb.Model.Concrete.User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("ParkingATHWeb.Model.Concrete.Message", b =>
                {
                    b.HasOne("ParkingATHWeb.Model.Concrete.User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.HasOne("ParkingATHWeb.Model.Concrete.Token")
                        .WithMany()
                        .HasForeignKey("ViewInBrowserTokenId");
                });

            modelBuilder.Entity("ParkingATHWeb.Model.Concrete.Order", b =>
                {
                    b.HasOne("ParkingATHWeb.Model.Concrete.PriceTreshold")
                        .WithMany()
                        .HasForeignKey("PriceTresholdId");

                    b.HasOne("ParkingATHWeb.Model.Concrete.User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("ParkingATHWeb.Model.Concrete.PortalMessage", b =>
                {
                    b.HasOne("ParkingATHWeb.Model.Concrete.PortalMessage")
                        .WithMany()
                        .HasForeignKey("PreviousMessageId");

                    b.HasOne("ParkingATHWeb.Model.Concrete.User")
                        .WithMany()
                        .HasForeignKey("ReceiverUserId");

                    b.HasOne("ParkingATHWeb.Model.Concrete.User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("ParkingATHWeb.Model.Concrete.User", b =>
                {
                    b.HasOne("ParkingATHWeb.Model.Concrete.Token")
                        .WithMany()
                        .HasForeignKey("PasswordChangeTokenId");

                    b.HasOne("ParkingATHWeb.Model.Concrete.Token")
                        .WithOne()
                        .HasForeignKey("ParkingATHWeb.Model.Concrete.User", "SelfDeleteTokenId");
                });

            modelBuilder.Entity("ParkingATHWeb.Model.Concrete.UserPreferences", b =>
                {
                    b.HasOne("ParkingATHWeb.Model.Concrete.User")
                        .WithOne()
                        .HasForeignKey("ParkingATHWeb.Model.Concrete.UserPreferences", "UserId");
                });

            modelBuilder.Entity("ParkingATHWeb.Model.Concrete.WeatherInfo", b =>
                {
                    b.HasOne("ParkingATHWeb.Model.Concrete.Weather")
                        .WithMany()
                        .HasForeignKey("WeatherId");
                });
        }
    }
}
