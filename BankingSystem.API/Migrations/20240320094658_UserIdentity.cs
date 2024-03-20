using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BankingSystem.API.Migrations
{
    /// <inheritdoc />
    public partial class UserIdentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("654cb1eb-a52c-4c15-8fff-027bb86b39f0"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("c1969721-fd42-4c8d-8279-73eb0602a5e9"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("c657facd-ed30-4ffb-9203-bbf1096ef1da"));

            migrationBuilder.AddColumn<int>(
                name: "AccessFailedCount",
                table: "Users",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EmailConfirmed",
                table: "Users",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "LockoutEnabled",
                table: "Users",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LockoutEnd",
                table: "Users",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedEmail",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedUserName",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PhoneNumberConfirmed",
                table: "Users",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "SecurityStamp",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TwoFactorEnabled",
                table: "Users",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "AccessFailedCount", "Address", "ConcurrencyStamp", "CreatedAt", "DateOfBirth", "Email", "EmailConfirmed", "Fullname", "Id", "LockoutEnabled", "LockoutEnd", "ModifiedAt", "NormalizedEmail", "NormalizedUserName", "Password", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "UserType", "Username" },
                values: new object[,]
                {
                    { new Guid("3e008792-1507-49d6-ae9b-db0280a69951"), 0, "Kathmandu", "17a92e6d-44ef-4885-9c4a-9e561c019e5b", new DateTime(2024, 3, 20, 9, 46, 57, 914, DateTimeKind.Utc).AddTicks(431), new DateTime(2000, 3, 23, 16, 58, 25, 342, DateTimeKind.Utc), "accountUser@gmail.com", false, "Account Holder", "9d16a167-a919-4bd0-bda9-5f452a3b589c", false, null, new DateTime(2024, 3, 20, 9, 46, 57, 914, DateTimeKind.Utc).AddTicks(456), null, null, "$2b$10$gXYfVW.8yCNdS.7tFFAwbuT2SnzIU0tjjYjSaytG9Z1sJ6/7EM4.i", null, "9830274849", false, "c4ed2d74-21ce-45f5-b814-7b4772bf3a39", false, null, 0, "user" },
                    { new Guid("ed5db673-53ab-4ac3-a441-8f5b2ba93361"), 0, "Gothatar, Kathmandu", "ac43dbbe-42b7-4001-b588-3a7b1f3192b8", new DateTime(2024, 3, 20, 9, 46, 57, 601, DateTimeKind.Utc).AddTicks(5958), new DateTime(2002, 8, 20, 16, 58, 25, 342, DateTimeKind.Utc), "subritiaryal13@gmail.com", false, "Subriti Aryal", "e1fe81c3-ad21-48dc-8b59-c01b8fa07cd2", false, null, new DateTime(2024, 3, 20, 9, 46, 57, 601, DateTimeKind.Utc).AddTicks(6007), null, null, "$2b$10$5v3kSWwbdSH7pFY426qCp.60B5EhF7ZtWdXRGulf44ItvpVem5JOK", null, "9843346520", false, "173578a5-61f6-4e71-8a2a-226330912d96", false, null, 0, "subs" },
                    { new Guid("eee3dcbb-5de0-4ea6-8e1c-4c219c253c86"), 0, "Kathmandu", "0df0bd11-b855-4349-9332-e470cd335097", new DateTime(2024, 3, 20, 9, 46, 57, 740, DateTimeKind.Utc).AddTicks(6099), new DateTime(2000, 3, 23, 16, 58, 25, 342, DateTimeKind.Utc), "teller@gmail.com", false, "Teller Person", "0dc4952a-9d27-4872-ada9-59320da0bd1a", false, null, new DateTime(2024, 3, 20, 9, 46, 57, 740, DateTimeKind.Utc).AddTicks(6120), null, null, "$2b$10$U.fpfdedWF9/TqRO3hblwu/2IzRvDIVksGnIBZjkA6PmfHMQcy3Ua", null, "9826274833", false, "40493b87-7e7c-487a-93f1-197bbddecb4a", false, null, 1, "admin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("3e008792-1507-49d6-ae9b-db0280a69951"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("ed5db673-53ab-4ac3-a441-8f5b2ba93361"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("eee3dcbb-5de0-4ea6-8e1c-4c219c253c86"));

            migrationBuilder.DropColumn(
                name: "AccessFailedCount",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "EmailConfirmed",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LockoutEnabled",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LockoutEnd",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "NormalizedEmail",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "NormalizedUserName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PhoneNumberConfirmed",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SecurityStamp",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TwoFactorEnabled",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Users");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Address", "CreatedAt", "DateOfBirth", "Email", "Fullname", "ModifiedAt", "Password", "PhoneNumber", "UserType", "Username" },
                values: new object[,]
                {
                    { new Guid("654cb1eb-a52c-4c15-8fff-027bb86b39f0"), "Gothatar, Kathmandu", new DateTime(2024, 3, 20, 9, 31, 21, 672, DateTimeKind.Utc).AddTicks(1460), new DateTime(2002, 8, 20, 16, 58, 25, 342, DateTimeKind.Utc), "subritiaryal13@gmail.com", "Subriti Aryal", new DateTime(2024, 3, 20, 9, 31, 21, 672, DateTimeKind.Utc).AddTicks(1479), "$2b$10$BbvablsgMoM/xF8yyCK/5Oasw6oPF7RSLqufkFxxSDonNor4zD4g6", "9843346520", 0, "subs" },
                    { new Guid("c1969721-fd42-4c8d-8279-73eb0602a5e9"), "Kathmandu", new DateTime(2024, 3, 20, 9, 31, 21, 782, DateTimeKind.Utc).AddTicks(9697), new DateTime(2000, 3, 23, 16, 58, 25, 342, DateTimeKind.Utc), "teller@gmail.com", "Teller Person", new DateTime(2024, 3, 20, 9, 31, 21, 782, DateTimeKind.Utc).AddTicks(9713), "$2b$10$ApVs03mBfoK2UQJz6QDYCuu3gUVQzza296tidDGR0YVddQHy0cgTK", "9826274833", 1, "admin" },
                    { new Guid("c657facd-ed30-4ffb-9203-bbf1096ef1da"), "Kathmandu", new DateTime(2024, 3, 20, 9, 31, 21, 898, DateTimeKind.Utc).AddTicks(7339), new DateTime(2000, 3, 23, 16, 58, 25, 342, DateTimeKind.Utc), "accountUser@gmail.com", "Account Holder", new DateTime(2024, 3, 20, 9, 31, 21, 898, DateTimeKind.Utc).AddTicks(7353), "$2b$10$k9umizpKb3bw9qU8vjVTB.8w3sanv2o8zpqrntQegnE53PbwhiavO", "9830274849", 0, "user" }
                });
        }
    }
}
