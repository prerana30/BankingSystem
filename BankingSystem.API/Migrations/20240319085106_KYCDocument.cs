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
                name: "KycDocument",
                columns: table => new
                {
                    KYCId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    FatherName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    MotherName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    GrandFatherName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
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

            migrationBuilder.CreateIndex(
                name: "IX_KycDocument_UserId",
                table: "KycDocument",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KycDocument");
        }
    }
}
