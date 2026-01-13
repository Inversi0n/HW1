using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Orders.Migrations
{
    /// <inheritdoc />
    public partial class OrderConfirmed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsHandled",
                table: "OutboxMessages");

            migrationBuilder.RenameColumn(
                name: "Value",
                table: "OutboxMessages",
                newName: "Payload");

            migrationBuilder.AddColumn<DateTime>(
                name: "ProcessedAt",
                table: "OutboxMessages",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ConfirmedAt",
                table: "Orders",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OutboxMessages_MessageType_ProcessedAt",
                table: "OutboxMessages",
                columns: new[] { "MessageType", "ProcessedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_Customers_Email",
                table: "Customers",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_OutboxMessages_MessageType_ProcessedAt",
                table: "OutboxMessages");

            migrationBuilder.DropIndex(
                name: "IX_Customers_Email",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "ProcessedAt",
                table: "OutboxMessages");

            migrationBuilder.DropColumn(
                name: "ConfirmedAt",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "Payload",
                table: "OutboxMessages",
                newName: "Value");

            migrationBuilder.AddColumn<bool>(
                name: "IsHandled",
                table: "OutboxMessages",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
