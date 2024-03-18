using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BankingSystem.API.Migrations
{
    /// <inheritdoc />
    public partial class TransactionIntial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Transaction",
                columns: table => new
                {
                    TransactionId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AccountId = table.Column<int>(type: "integer", nullable: false),
                    TransactionType = table.Column<int>(type: "integer", nullable: false),
                    Amount = table.Column<double>(type: "double precision", maxLength: 50, nullable: false),
                    TransactionTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TransactionRemarks = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.TransactionId);
                });

            migrationBuilder.InsertData(
                table: "Transaction",
                columns: new[] { "TransactionId", "AccountId", "Amount", "TransactionRemarks", "TransactionTime", "TransactionType" },
                values: new object[] { 1, 1, 20.289999999999999, "Personal Use", new DateTime(2024, 3, 18, 18, 6, 51, 66, DateTimeKind.Utc).AddTicks(5912), 0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transaction");
        }
    }
}
