using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace ParkingATHWeb.Model.Migrations
{
    public partial class Weather : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_GateUsage_User_UserId", table: "GateUsage");
            migrationBuilder.DropForeignKey(name: "FK_Message_Token_ViewInBrowserTokenId", table: "Message");
            migrationBuilder.DropForeignKey(name: "FK_Order_PriceTreshold_PriceTresholdId", table: "Order");
            migrationBuilder.DropForeignKey(name: "FK_Order_User_UserId", table: "Order");
            migrationBuilder.DropForeignKey(name: "FK_User_Token_SelfDeleteTokenId", table: "User");
            migrationBuilder.DropForeignKey(name: "FK_User_UserPreferences_UserPreferencesId", table: "User");
            migrationBuilder.CreateTable(
                name: "Weather",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateOfRead = table.Column<DateTime>(nullable: false),
                    Humidity = table.Column<double>(nullable: false),
                    Pressure = table.Column<double>(nullable: false),
                    Temperature = table.Column<double>(nullable: false),
                    TemperatureMax = table.Column<double>(nullable: false),
                    TemperatureMin = table.Column<double>(nullable: false),
                    ValidToDate = table.Column<DateTime>(nullable: false),
                    WeatherDescription = table.Column<string>(nullable: true),
                    WeatherMain = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Weather", x => x.Id);
                });
            migrationBuilder.AddForeignKey(
                name: "FK_GateUsage_User_UserId",
                table: "GateUsage",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_Message_Token_ViewInBrowserTokenId",
                table: "Message",
                column: "ViewInBrowserTokenId",
                principalTable: "Token",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_Order_PriceTreshold_PriceTresholdId",
                table: "Order",
                column: "PriceTresholdId",
                principalTable: "PriceTreshold",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_Order_User_UserId",
                table: "Order",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_User_Token_SelfDeleteTokenId",
                table: "User",
                column: "SelfDeleteTokenId",
                principalTable: "Token",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_User_UserPreferences_UserPreferencesId",
                table: "User",
                column: "UserPreferencesId",
                principalTable: "UserPreferences",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_GateUsage_User_UserId", table: "GateUsage");
            migrationBuilder.DropForeignKey(name: "FK_Message_Token_ViewInBrowserTokenId", table: "Message");
            migrationBuilder.DropForeignKey(name: "FK_Order_PriceTreshold_PriceTresholdId", table: "Order");
            migrationBuilder.DropForeignKey(name: "FK_Order_User_UserId", table: "Order");
            migrationBuilder.DropForeignKey(name: "FK_User_Token_SelfDeleteTokenId", table: "User");
            migrationBuilder.DropForeignKey(name: "FK_User_UserPreferences_UserPreferencesId", table: "User");
            migrationBuilder.DropTable("Weather");
            migrationBuilder.AddForeignKey(
                name: "FK_GateUsage_User_UserId",
                table: "GateUsage",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_Message_Token_ViewInBrowserTokenId",
                table: "Message",
                column: "ViewInBrowserTokenId",
                principalTable: "Token",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_Order_PriceTreshold_PriceTresholdId",
                table: "Order",
                column: "PriceTresholdId",
                principalTable: "PriceTreshold",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_Order_User_UserId",
                table: "Order",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_User_Token_SelfDeleteTokenId",
                table: "User",
                column: "SelfDeleteTokenId",
                principalTable: "Token",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_User_UserPreferences_UserPreferencesId",
                table: "User",
                column: "UserPreferencesId",
                principalTable: "UserPreferences",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
