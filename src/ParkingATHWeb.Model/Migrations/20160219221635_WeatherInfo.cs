using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace ParkingATHWeb.Model.Migrations
{
    public partial class WeatherInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_GateUsage_User_UserId", table: "GateUsage");
            migrationBuilder.DropForeignKey(name: "FK_Message_Token_ViewInBrowserTokenId", table: "Message");
            migrationBuilder.DropForeignKey(name: "FK_Order_PriceTreshold_PriceTresholdId", table: "Order");
            migrationBuilder.DropForeignKey(name: "FK_Order_User_UserId", table: "Order");
            migrationBuilder.DropForeignKey(name: "FK_User_Token_SelfDeleteTokenId", table: "User");
            migrationBuilder.DropForeignKey(name: "FK_User_UserPreferences_UserPreferencesId", table: "User");
            migrationBuilder.DropColumn(name: "TemperatureMax", table: "Weather");
            migrationBuilder.DropColumn(name: "TemperatureMin", table: "Weather");
            migrationBuilder.DropColumn(name: "WeatherDescription", table: "Weather");
            migrationBuilder.DropColumn(name: "WeatherId", table: "Weather");
            migrationBuilder.DropColumn(name: "WeatherMain", table: "Weather");
            migrationBuilder.CreateTable(
                name: "WeatherInfo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    WeatherConditionId = table.Column<int>(nullable: false),
                    WeatherDescription = table.Column<string>(nullable: true),
                    WeatherId = table.Column<Guid>(nullable: false),
                    WeatherMain = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WeatherInfo_Weather_WeatherId",
                        column: x => x.WeatherId,
                        principalTable: "Weather",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
            migrationBuilder.DropTable("WeatherInfo");
            migrationBuilder.AddColumn<double>(
                name: "TemperatureMax",
                table: "Weather",
                nullable: false,
                defaultValue: 0);
            migrationBuilder.AddColumn<double>(
                name: "TemperatureMin",
                table: "Weather",
                nullable: false,
                defaultValue: 0);
            migrationBuilder.AddColumn<string>(
                name: "WeatherDescription",
                table: "Weather",
                nullable: true);
            migrationBuilder.AddColumn<int>(
                name: "WeatherId",
                table: "Weather",
                nullable: false,
                defaultValue: 0);
            migrationBuilder.AddColumn<string>(
                name: "WeatherMain",
                table: "Weather",
                nullable: true);
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
