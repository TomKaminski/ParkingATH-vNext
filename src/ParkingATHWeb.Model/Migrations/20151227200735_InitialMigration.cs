using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Metadata;

namespace ParkingATHWeb.Model.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PriceTreshold",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MinCharges = table.Column<int>(nullable: false),
                    PricePerCharge = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceTreshold", x => x.Id);
                });
            migrationBuilder.CreateTable(
                name: "Token",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SecureToken = table.Column<Guid>(nullable: false),
                    TokenType = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    ValidTo = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Token", x => x.Id);
                });
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Charges = table.Column<int>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    EmailChangeTokenId = table.Column<long>(nullable: true),
                    IsAdmin = table.Column<bool>(nullable: false),
                    LastName = table.Column<string>(nullable: true),
                    LockedOut = table.Column<bool>(nullable: false),
                    LockedTo = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    PasswordChangeTokenId = table.Column<long>(nullable: true),
                    PasswordHash = table.Column<string>(nullable: true),
                    PasswordSalt = table.Column<string>(nullable: true),
                    UnsuccessfulLoginAttempts = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Token_EmailChangeTokenId",
                        column: x => x.EmailChangeTokenId,
                        principalTable: "Token",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_User_Token_PasswordChangeTokenId",
                        column: x => x.PasswordChangeTokenId,
                        principalTable: "Token",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });
            migrationBuilder.CreateTable(
                name: "GateUsage",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateOfUse = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GateUsage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GateUsage_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateTable(
                name: "Message",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    BCC = table.Column<string>(nullable: true),
                    CC = table.Column<string>(nullable: true),
                    DisplayFrom = table.Column<string>(nullable: true),
                    From = table.Column<string>(nullable: true),
                    MessageParameters = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    To = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Message", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Message_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(nullable: false),
                    ExtOrderId = table.Column<Guid>(nullable: false),
                    NumOfCharges = table.Column<int>(nullable: false),
                    OrderPlace = table.Column<int>(nullable: false),
                    OrderState = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    PriceTresholdId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Order_PriceTreshold_PriceTresholdId",
                        column: x => x.PriceTresholdId,
                        principalTable: "PriceTreshold",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Order_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("GateUsage");
            migrationBuilder.DropTable("Message");
            migrationBuilder.DropTable("Order");
            migrationBuilder.DropTable("PriceTreshold");
            migrationBuilder.DropTable("User");
            migrationBuilder.DropTable("Token");
        }
    }
}
