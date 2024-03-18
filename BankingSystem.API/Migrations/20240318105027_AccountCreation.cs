using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BankingSystem.API.Migrations
{
    /// <inheritdoc />
    public partial class AccountCreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    AccountId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    AccountNumber = table.Column<long>(type: "bigint", maxLength: 24, nullable: false),
                    Balance = table.Column<long>(type: "bigint", nullable: false),
                    AtmCardNum = table.Column<long>(type: "bigint", nullable: false),
                    AtmCardPin = table.Column<int>(type: "integer", maxLength: 4, nullable: false),
                    AccountCreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AccountCreatedBy = table.Column<int>(type: "integer", nullable: false),
                    AccountModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AccountModifiedBy = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.AccountId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");
        }
    }
}
