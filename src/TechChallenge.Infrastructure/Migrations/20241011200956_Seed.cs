using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TechChallenge.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Seed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Region",
                columns: new[] { "DDD", "Location" },
                values: new object[,]
                {
                    { 11, "São Paulo" },
                    { 12, "Rio Preto" },
                    { 19, "Campinas" }
                });

            migrationBuilder.InsertData(
                table: "Contact",
                columns: new[] { "Guid", "Email", "Name", "Phone", "RegionDDD" },
                values: new object[,]
                {
                    { new Guid("18cbd524-dae3-4655-8bbe-168ddb1460e7"), "joao@gmail.com", "Joao", "999988888", 12 },
                    { new Guid("2f2be063-3b12-41ab-b0b1-1ad8f2a0609e"), "pedro@gmail.com", "Pedro", "999977778", 19 },
                    { new Guid("6f107b50-e7c6-43ab-b88f-211895f6e4f8"), "paulo@gmail.com", "Paulo", "321456888", 11 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Contact",
                keyColumn: "Guid",
                keyValue: new Guid("18cbd524-dae3-4655-8bbe-168ddb1460e7"));

            migrationBuilder.DeleteData(
                table: "Contact",
                keyColumn: "Guid",
                keyValue: new Guid("2f2be063-3b12-41ab-b0b1-1ad8f2a0609e"));

            migrationBuilder.DeleteData(
                table: "Contact",
                keyColumn: "Guid",
                keyValue: new Guid("6f107b50-e7c6-43ab-b88f-211895f6e4f8"));

            migrationBuilder.DeleteData(
                table: "Region",
                keyColumn: "DDD",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Region",
                keyColumn: "DDD",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Region",
                keyColumn: "DDD",
                keyValue: 19);
        }
    }
}
