using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BankingSystem.API.Migrations
{
    /// <inheritdoc />
    public partial class KYCDocument : Migration
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
                name: "KycDocument",
                columns: table => new
                {
                    KYCId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    FatherName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    MotherName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    GrandFatherName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    UserImageFile = table.Column<string>(type: "text", nullable: false),
                    CitizenshipImageFile = table.Column<string>(type: "text", nullable: false),
                    PermanentAddress = table.Column<string>(type: "text", nullable: false),
                    UploadedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsApproved = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KycDocument", x => x.KYCId);
                    table.ForeignKey(
                        name: "FK_KycDocument_Users_UserId",
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
                    { new Guid("15385d22-8aaa-411e-889c-b6913d526cac"), "Kathmandu", new DateTime(2024, 3, 19, 9, 57, 29, 112, DateTimeKind.Utc).AddTicks(241), new DateTime(2000, 3, 23, 16, 58, 25, 342, DateTimeKind.Utc), "accountUser@gmail.com", "Account Holder", new DateTime(2024, 3, 19, 9, 57, 29, 112, DateTimeKind.Utc).AddTicks(257), "$2b$10$eSWdUO2KkFNF8wJRUpon9.BdhCj/NtOEw8WTbeF6MZadM9Ue7bNVq", 0, "user" },
                    { new Guid("2ce78094-f04b-4dc2-9cc1-ea66d29384de"), "Gothatar, Kathmandu", new DateTime(2024, 3, 19, 9, 57, 28, 914, DateTimeKind.Utc).AddTicks(898), new DateTime(2002, 8, 20, 16, 58, 25, 342, DateTimeKind.Utc), "subritiaryal13@gmail.com", "Subriti Aryal", new DateTime(2024, 3, 19, 9, 57, 28, 914, DateTimeKind.Utc).AddTicks(915), "$2b$10$RNPCVI8DorJwtTv0GKnpN.ndArGyzXlKveHvSan38rHAFIqA34nH.", 0, "subs" },
                    { new Guid("efe8b5d1-d582-4d23-b1b4-8f27d8745721"), "Kathmandu", new DateTime(2024, 3, 19, 9, 57, 29, 9, DateTimeKind.Utc).AddTicks(1821), new DateTime(2000, 3, 23, 16, 58, 25, 342, DateTimeKind.Utc), "teller@gmail.com", "Teller Person", new DateTime(2024, 3, 19, 9, 57, 29, 9, DateTimeKind.Utc).AddTicks(1840), "$2b$10$b55xyuZVxOS9yaMGbDA1ducVX5IK9cT83teJHMvvFDY4FvJiltb9i", 1, "admin" }
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
                name: "IX_KycDocument_UserId",
                table: "KycDocument",
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
                name: "KycDocument");

            migrationBuilder.DropTable(
                name: "Transaction");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
