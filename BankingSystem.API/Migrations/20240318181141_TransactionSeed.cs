using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BankingSystem.API.Migrations
{
    /// <inheritdoc />
    public partial class TransactionSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Transaction",
                keyColumn: "TransactionId",
                keyValue: 1,
                column: "TransactionTime",
                value: new DateTime(2024, 3, 18, 18, 11, 40, 253, DateTimeKind.Utc).AddTicks(6365));

            migrationBuilder.InsertData(
                table: "Transaction",
                columns: new[] { "TransactionId", "AccountId", "Amount", "TransactionRemarks", "TransactionTime", "TransactionType" },
                values: new object[,]
                {
                    { 2, 1, 10.09, "Personal Use WWW", new DateTime(2024, 3, 18, 18, 11, 40, 253, DateTimeKind.Utc).AddTicks(6368), 1 },
                    { 3, 1, 30.289999999999999, "Personalwqq Use", new DateTime(2024, 3, 18, 18, 11, 40, 253, DateTimeKind.Utc).AddTicks(6370), 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Transaction",
                keyColumn: "TransactionId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Transaction",
                keyColumn: "TransactionId",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "Transaction",
                keyColumn: "TransactionId",
                keyValue: 1,
                column: "TransactionTime",
                value: new DateTime(2024, 3, 18, 18, 6, 51, 66, DateTimeKind.Utc).AddTicks(5912));
        }
    }
}
