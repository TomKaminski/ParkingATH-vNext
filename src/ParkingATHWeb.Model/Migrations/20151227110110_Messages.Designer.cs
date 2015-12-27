using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using ParkingATHWeb.Model;

namespace ParkingATHWeb.Model.Migrations
{
    [DbContext(typeof(ParkingAthContext))]
    [Migration("20151227110110_Messages")]
    partial class Messages
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

                    b.Property<string>("HtmlBody");

                    b.Property<string>("Title");

                    b.Property<string>("To");

                    b.Property<int>("Type");

                    b.Property<int>("UserId");

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

                    b.Property<Guid>("SecureToken");

                    b.Property<int>("TokenType");

                    b.Property<int>("UserId");

                    b.Property<DateTime>("ValidTo");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("ParkingATHWeb.Model.Concrete.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Charges");

                    b.Property<string>("Email");

                    b.Property<long?>("EmailChangeTokenId");

                    b.Property<bool>("IsAdmin");

                    b.Property<string>("LastName");

                    b.Property<bool>("LockedOut");

                    b.Property<DateTime?>("LockedTo");

                    b.Property<string>("Name");

                    b.Property<long?>("PasswordChangeTokenId");

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PasswordSalt");

                    b.Property<int>("UnsuccessfulLoginAttempts");

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

            modelBuilder.Entity("ParkingATHWeb.Model.Concrete.User", b =>
                {
                    b.HasOne("ParkingATHWeb.Model.Concrete.Token")
                        .WithOne()
                        .HasForeignKey("ParkingATHWeb.Model.Concrete.User", "EmailChangeTokenId");

                    b.HasOne("ParkingATHWeb.Model.Concrete.Token")
                        .WithMany()
                        .HasForeignKey("PasswordChangeTokenId");
                });
        }
    }
}
