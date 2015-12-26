using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace ParkingATHWeb.Model.Migrations
{
    public partial class LockedToNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_GateUsage_User_UserId", table: "GateUsage");
            migrationBuilder.DropForeignKey(name: "FK_Order_PriceTreshold_PriceTresholdId", table: "Order");
            migrationBuilder.DropForeignKey(name: "FK_Order_User_UserId", table: "Order");
            migrationBuilder.DropForeignKey(name: "FK_User_Token_PasswordChangeTokenId", table: "User");
            migrationBuilder.DropColumn(name: "EncryptedToken", table: "Token");
            migrationBuilder.AlterColumn<DateTime>(
                name: "LockedTo",
                table: "User",
                nullable: true);
            migrationBuilder.AddColumn<Guid>(
                name: "SecureToken",
                table: "Token",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
            migrationBuilder.AddColumn<int>(
                name: "TokenType",
                table: "Token",
                nullable: false,
                defaultValue: 0);
            migrationBuilder.AddForeignKey(
                name: "FK_GateUsage_User_UserId",
                table: "GateUsage",
                column: "UserId",
                principalTable: "User",
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
                name: "FK_User_Token_PasswordChangeTokenId",
                table: "User",
                column: "PasswordChangeTokenId",
                principalTable: "Token",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_GateUsage_User_UserId", table: "GateUsage");
            migrationBuilder.DropForeignKey(name: "FK_Order_PriceTreshold_PriceTresholdId", table: "Order");
            migrationBuilder.DropForeignKey(name: "FK_Order_User_UserId", table: "Order");
            migrationBuilder.DropForeignKey(name: "FK_User_Token_PasswordChangeTokenId", table: "User");
            migrationBuilder.DropColumn(name: "SecureToken", table: "Token");
            migrationBuilder.DropColumn(name: "TokenType", table: "Token");
            migrationBuilder.AlterColumn<DateTime>(
                name: "LockedTo",
                table: "User",
                nullable: false);
            migrationBuilder.AddColumn<string>(
                name: "EncryptedToken",
                table: "Token",
                nullable: true);
            migrationBuilder.AddForeignKey(
                name: "FK_GateUsage_User_UserId",
                table: "GateUsage",
                column: "UserId",
                principalTable: "User",
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
                name: "FK_User_Token_PasswordChangeTokenId",
                table: "User",
                column: "PasswordChangeTokenId",
                principalTable: "Token",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
