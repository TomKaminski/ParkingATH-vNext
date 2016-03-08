using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace ParkingATHWeb.Model.Migrations
{
    public partial class ChartPreferences : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_GateUsage_User_UserId", table: "GateUsage");
            migrationBuilder.DropForeignKey(name: "FK_Message_Token_ViewInBrowserTokenId", table: "Message");
            migrationBuilder.DropForeignKey(name: "FK_Order_PriceTreshold_PriceTresholdId", table: "Order");
            migrationBuilder.DropForeignKey(name: "FK_Order_User_UserId", table: "Order");
            migrationBuilder.DropForeignKey(name: "FK_PortalMessage_User_UserId", table: "PortalMessage");
            migrationBuilder.DropForeignKey(name: "FK_User_Token_SelfDeleteTokenId", table: "User");
            migrationBuilder.DropForeignKey(name: "FK_UserPreferences_User_UserId", table: "UserPreferences");
            migrationBuilder.DropForeignKey(name: "FK_WeatherInfo_Weather_WeatherId", table: "WeatherInfo");
            migrationBuilder.AddColumn<DateTime>(
                name: "GateUsagesChartEndDate",
                table: "UserPreferences",
                nullable: true);
            migrationBuilder.AddColumn<int>(
                name: "GateUsagesChartGranuality",
                table: "UserPreferences",
                nullable: true);
            migrationBuilder.AddColumn<DateTime>(
                name: "GateUsagesChartStartDate",
                table: "UserPreferences",
                nullable: true);
            migrationBuilder.AddColumn<DateTime>(
                name: "OrdersChartEndDate",
                table: "UserPreferences",
                nullable: true);
            migrationBuilder.AddColumn<int>(
                name: "OrdersChartGranuality",
                table: "UserPreferences",
                nullable: true);
            migrationBuilder.AddColumn<DateTime>(
                name: "OrdersChartStartDate",
                table: "UserPreferences",
                nullable: true);
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
                name: "FK_PortalMessage_User_UserId",
                table: "PortalMessage",
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
                name: "FK_UserPreferences_User_UserId",
                table: "UserPreferences",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_WeatherInfo_Weather_WeatherId",
                table: "WeatherInfo",
                column: "WeatherId",
                principalTable: "Weather",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_GateUsage_User_UserId", table: "GateUsage");
            migrationBuilder.DropForeignKey(name: "FK_Message_Token_ViewInBrowserTokenId", table: "Message");
            migrationBuilder.DropForeignKey(name: "FK_Order_PriceTreshold_PriceTresholdId", table: "Order");
            migrationBuilder.DropForeignKey(name: "FK_Order_User_UserId", table: "Order");
            migrationBuilder.DropForeignKey(name: "FK_PortalMessage_User_UserId", table: "PortalMessage");
            migrationBuilder.DropForeignKey(name: "FK_User_Token_SelfDeleteTokenId", table: "User");
            migrationBuilder.DropForeignKey(name: "FK_UserPreferences_User_UserId", table: "UserPreferences");
            migrationBuilder.DropForeignKey(name: "FK_WeatherInfo_Weather_WeatherId", table: "WeatherInfo");
            migrationBuilder.DropColumn(name: "GateUsagesChartEndDate", table: "UserPreferences");
            migrationBuilder.DropColumn(name: "GateUsagesChartGranuality", table: "UserPreferences");
            migrationBuilder.DropColumn(name: "GateUsagesChartStartDate", table: "UserPreferences");
            migrationBuilder.DropColumn(name: "OrdersChartEndDate", table: "UserPreferences");
            migrationBuilder.DropColumn(name: "OrdersChartGranuality", table: "UserPreferences");
            migrationBuilder.DropColumn(name: "OrdersChartStartDate", table: "UserPreferences");
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
                name: "FK_PortalMessage_User_UserId",
                table: "PortalMessage",
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
                name: "FK_UserPreferences_User_UserId",
                table: "UserPreferences",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_WeatherInfo_Weather_WeatherId",
                table: "WeatherInfo",
                column: "WeatherId",
                principalTable: "Weather",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
