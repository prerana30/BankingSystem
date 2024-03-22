using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BankingSystem.API.Migrations
{
    /// <inheritdoc />
    public partial class seed_users : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "SystemUser",
                columns: new[] { "UserId", "Address", "CreatedAt", "DateOfBirth", "Email", "Fullname", "ModifiedAt", "Password", "UserType", "Username" },
                values: new object[,]
                {
                    { new Guid("877aabaf-4d57-48a4-b082-d463727e6e9d"), "Gothatar, Kathmandu", new DateTime(2024, 3, 21, 4, 37, 41, 139, DateTimeKind.Utc).AddTicks(5019), new DateTime(2002, 8, 20, 16, 58, 25, 342, DateTimeKind.Utc), "subritiaryal13@gmail.com", "Subriti Aryal", new DateTime(2024, 3, 21, 4, 37, 41, 139, DateTimeKind.Utc).AddTicks(5036), "$2b$10$pcdbIwIooJRwkETq6SSbY.6HYNDxl0gUQBEliwJ4e7oGR6GfKCV/K", 0, "subs" },
                    { new Guid("e03bae37-9270-457d-8829-fa51a825be76"), "Kathmandu", new DateTime(2024, 3, 21, 4, 37, 41, 234, DateTimeKind.Utc).AddTicks(9920), new DateTime(2000, 3, 23, 16, 58, 25, 342, DateTimeKind.Utc), "teller@gmail.com", "Teller Person", new DateTime(2024, 3, 21, 4, 37, 41, 234, DateTimeKind.Utc).AddTicks(9934), "$2b$10$uR.qZ.nj1vYSUwRSiMVUk.Hq4takkc1XR0/XCzoczjTm7Er0/HjuK", 1, "admin" },
                    { new Guid("ebfa3176-bde0-47bf-9c4a-8a284633989f"), "Kathmandu", new DateTime(2024, 3, 21, 4, 37, 41, 326, DateTimeKind.Utc).AddTicks(2983), new DateTime(2000, 3, 23, 16, 58, 25, 342, DateTimeKind.Utc), "accountUser@gmail.com", "Account Holder", new DateTime(2024, 3, 21, 4, 37, 41, 326, DateTimeKind.Utc).AddTicks(2999), "$2b$10$N6cpU5rnKgRIXl/Rj1.fzOF4da7VDS5pFwGlDAReW/ghYGeWY1CMy", 0, "user" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SystemUser",
                keyColumn: "UserId",
                keyValue: new Guid("877aabaf-4d57-48a4-b082-d463727e6e9d"));

            migrationBuilder.DeleteData(
                table: "SystemUser",
                keyColumn: "UserId",
                keyValue: new Guid("e03bae37-9270-457d-8829-fa51a825be76"));

            migrationBuilder.DeleteData(
                table: "SystemUser",
                keyColumn: "UserId",
                keyValue: new Guid("ebfa3176-bde0-47bf-9c4a-8a284633989f"));
        }
    }
}
