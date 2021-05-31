using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EdProject.DAL.Migrations
{
    public partial class changes_in_seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2c5ed513-7a3c-4cb7-8b3e-fd730858460d", "AQAAAAEAACcQAAAAEJJ8HW1SwMPnbHU8na0CVmkQJiRH2+39JejJId/1K9cM6x1AQz7kecrqTvIdEna6jw==", "3ed46612-00ec-486a-8d5a-54c6e88cff07" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2d32d839-4002-417b-92a7-0e18ff8a29cf", "AQAAAAEAACcQAAAAEGtH15/FC4JHrpl0QyjlC8o5DqXF/UPZmQkQrI5CB0tSUd1hEtuYINeIH9Hs+HOsnA==", "0cfe3820-11d6-4f55-8c75-32f13e0b209a" });

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(2021, 5, 31, 11, 51, 42, 943, DateTimeKind.Local).AddTicks(4667));

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedAt",
                value: new DateTime(2021, 5, 31, 11, 51, 42, 943, DateTimeKind.Local).AddTicks(5226));

            migrationBuilder.UpdateData(
                table: "Editions",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedAt", "Currency" },
                values: new object[] { new DateTime(2021, 5, 31, 11, 51, 42, 940, DateTimeKind.Local).AddTicks(8784), "USD" });

            migrationBuilder.UpdateData(
                table: "Editions",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedAt", "Currency" },
                values: new object[] { new DateTime(2021, 5, 31, 11, 51, 42, 943, DateTimeKind.Local).AddTicks(3420), "USD" });

            migrationBuilder.UpdateData(
                table: "Editions",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedAt", "Currency" },
                values: new object[] { new DateTime(2021, 5, 31, 11, 51, 42, 943, DateTimeKind.Local).AddTicks(3455), "USD" });

            migrationBuilder.UpdateData(
                table: "Editions",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedAt", "Currency" },
                values: new object[] { new DateTime(2021, 5, 31, 11, 51, 42, 943, DateTimeKind.Local).AddTicks(3461), "USD" });

            migrationBuilder.UpdateData(
                table: "Editions",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedAt", "Currency" },
                values: new object[] { new DateTime(2021, 5, 31, 11, 51, 42, 943, DateTimeKind.Local).AddTicks(3464), "USD" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fcb903bf-8d49-438a-acdc-6cb6989e54d7", "AQAAAAEAACcQAAAAEKDHyKP7gPkcz8zCndJh/qW0eLi78HOQIiWWp2YAT4vSaGABsAU8WyipHwOsqVQAOA==", "4cb0c2d4-8e5c-4ee3-8638-78da0f0fd7b4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8d70a07b-c471-4a30-9754-e0e996f4edc4", "AQAAAAEAACcQAAAAEHB6ZHWowQm1ylfKVmkYpoGZfVFF00JRqGnofEGkWS1iLWoSiN50fiHyNP5CcGtQHQ==", "3222d899-b27e-477b-bfe2-098e48beeb10" });

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(2021, 5, 28, 18, 26, 53, 420, DateTimeKind.Local).AddTicks(2283));

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedAt",
                value: new DateTime(2021, 5, 28, 18, 26, 53, 420, DateTimeKind.Local).AddTicks(2703));

            migrationBuilder.UpdateData(
                table: "Editions",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedAt", "Currency" },
                values: new object[] { new DateTime(2021, 5, 28, 18, 26, 53, 417, DateTimeKind.Local).AddTicks(7733), "UAH" });

            migrationBuilder.UpdateData(
                table: "Editions",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedAt", "Currency" },
                values: new object[] { new DateTime(2021, 5, 28, 18, 26, 53, 420, DateTimeKind.Local).AddTicks(1575), "UAH" });

            migrationBuilder.UpdateData(
                table: "Editions",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedAt", "Currency" },
                values: new object[] { new DateTime(2021, 5, 28, 18, 26, 53, 420, DateTimeKind.Local).AddTicks(1608), "UAH" });

            migrationBuilder.UpdateData(
                table: "Editions",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedAt", "Currency" },
                values: new object[] { new DateTime(2021, 5, 28, 18, 26, 53, 420, DateTimeKind.Local).AddTicks(1615), "UAH" });

            migrationBuilder.UpdateData(
                table: "Editions",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedAt", "Currency" },
                values: new object[] { new DateTime(2021, 5, 28, 18, 26, 53, 420, DateTimeKind.Local).AddTicks(1619), "EUR" });
        }
    }
}
