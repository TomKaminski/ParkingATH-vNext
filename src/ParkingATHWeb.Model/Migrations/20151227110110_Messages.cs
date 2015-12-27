using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace ParkingATHWeb.Model.Migrations
{
    public partial class Messages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_GateUsage_User_UserId", table: "GateUsage");
            migrationBuilder.DropForeignKey(name: "FK_Order_PriceTreshold_PriceTresholdId", table: "Order");
            migrationBuilder.DropForeignKey(name: "FK_Order_User_UserId", table: "Order");
            migrationBuilder.CreateTable(
                name: "Message",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    BCC = table.Column<string>(nullable: true),
                    CC = table.Column<string>(nullable: true),
                    DisplayFrom = table.Column<string>(nullable: true),
                    From = table.Column<string>(nullable: true),
                    HtmlBody = table.Column<string>(nullable: true),
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_GateUsage_User_UserId", table: "GateUsage");
            migrationBuilder.DropForeignKey(name: "FK_Order_PriceTreshold_PriceTresholdId", table: "Order");
            migrationBuilder.DropForeignKey(name: "FK_Order_User_UserId", table: "Order");
            migrationBuilder.DropTable("Message");
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
        }
    }
}
