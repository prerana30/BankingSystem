using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BankingSystem.API.Migrations
{
    /// <inheritdoc />
    public partial class ForeignKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Username = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Fullname = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false),
                    UserType = table.Column<int>(type: "integer", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    AccountId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    AccountNumber = table.Column<long>(type: "bigint", nullable: false),
                    Balance = table.Column<long>(type: "bigint", nullable: false),
                    AtmCardNum = table.Column<long>(type: "bigint", nullable: false),
                    AtmCardPin = table.Column<int>(type: "integer", nullable: false),
                    AccountCreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AccountCreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    AccountModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AccountModifiedBy = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.AccountId);
                    table.ForeignKey(
                        name: "FK_Accounts_Users_AccountCreatedBy",
                        column: x => x.AccountCreatedBy,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Accounts_Users_AccountModifiedBy",
                        column: x => x.AccountModifiedBy,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Accounts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transaction",
                columns: table => new
                {
                    TransactionId = table.Column<Guid>(type: "uuid", nullable: false),
                    AccountId = table.Column<Guid>(type: "uuid", nullable: false),
                    TransactionType = table.Column<int>(type: "integer", nullable: false),
                    Amount = table.Column<double>(type: "double precision", maxLength: 50, nullable: false),
                    TransactionTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TransactionRemarks = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.TransactionId);
                    table.ForeignKey(
                        name: "FK_Transaction_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Address", "CreatedAt", "DateOfBirth", "Email", "Fullname", "ModifiedAt", "Password", "UserType", "Username" },
                values: new object[,]
                {
                    { new Guid("10262b22-69d0-4126-a513-e887ae6dac61"), "Kathmandu", new DateTime(2024, 3, 19, 6, 57, 10, 602, DateTimeKind.Utc).AddTicks(682), new DateTime(2000, 3, 23, 16, 58, 25, 342, DateTimeKind.Utc), "accountUser@gmail.com", "Account Holder", new DateTime(2024, 3, 19, 6, 57, 10, 602, DateTimeKind.Utc).AddTicks(698), "$2b$10$.SfWGzq2faMIbJ4QO5uvteRCddoRHTnCSAM6afqHbLARL.WAmXoNi", 0, "user" },
                    { new Guid("33101bf4-23cf-4d25-9d49-d1a3607945b3"), "Gothatar, Kathmandu", new DateTime(2024, 3, 19, 6, 57, 10, 137, DateTimeKind.Utc).AddTicks(6429), new DateTime(2002, 8, 20, 16, 58, 25, 342, DateTimeKind.Utc), "subritiaryal13@gmail.com", "Subriti Aryal", new DateTime(2024, 3, 19, 6, 57, 10, 137, DateTimeKind.Utc).AddTicks(6446), "$2b$10$LG.NXzRS.y/VIF0Fkxhg0emd3q4I5n.H6FjxaW8fQSdLrI8z3iG7K", 0, "subs" },
                    { new Guid("e2a3d57f-0f34-4b71-a2f9-4c87777e6c2b"), "Kathmandu", new DateTime(2024, 3, 19, 6, 57, 10, 417, DateTimeKind.Utc).AddTicks(3035), new DateTime(2000, 3, 23, 16, 58, 25, 342, DateTimeKind.Utc), "teller@gmail.com", "Teller Person", new DateTime(2024, 3, 19, 6, 57, 10, 417, DateTimeKind.Utc).AddTicks(3062), "$2b$10$/x.zWa.Iv3Ri0UXzabb9TOzCRlWMQ0NRoyHp6hWsoQsSQWsU/.dMu", 1, "admin" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_AccountCreatedBy",
                table: "Accounts",
                column: "AccountCreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_AccountModifiedBy",
                table: "Accounts",
                column: "AccountModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_UserId",
                table: "Accounts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_AccountId",
                table: "Transaction",
                column: "AccountId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transaction");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
