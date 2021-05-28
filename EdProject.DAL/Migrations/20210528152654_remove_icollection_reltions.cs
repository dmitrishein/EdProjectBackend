using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EdProject.DAL.Migrations
{
    public partial class remove_icollection_reltions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                column: "CreatedAt",
                value: new DateTime(2021, 5, 28, 18, 26, 53, 417, DateTimeKind.Local).AddTicks(7733));

            migrationBuilder.UpdateData(
                table: "Editions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedAt",
                value: new DateTime(2021, 5, 28, 18, 26, 53, 420, DateTimeKind.Local).AddTicks(1575));

            migrationBuilder.UpdateData(
                table: "Editions",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedAt",
                value: new DateTime(2021, 5, 28, 18, 26, 53, 420, DateTimeKind.Local).AddTicks(1608));

            migrationBuilder.UpdateData(
                table: "Editions",
                keyColumn: "Id",
                keyValue: 4L,
                column: "CreatedAt",
                value: new DateTime(2021, 5, 28, 18, 26, 53, 420, DateTimeKind.Local).AddTicks(1615));

            migrationBuilder.UpdateData(
                table: "Editions",
                keyColumn: "Id",
                keyValue: 5L,
                column: "CreatedAt",
                value: new DateTime(2021, 5, 28, 18, 26, 53, 420, DateTimeKind.Local).AddTicks(1619));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "71afdc58-9be4-4ef4-97e6-8813402db7c2", "AQAAAAEAACcQAAAAEDCbwae6jlyUBZf4H1Eib295TYhvIg90qLeYQUK70Y1IHNWsMAKLnS04mCaNIT11sA==", "285808a9-4656-4182-90ee-4bfa65f06368" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f6162163-6800-45d2-a6fc-4dcd41c3718d", "AQAAAAEAACcQAAAAEMQJnPyGjPh2DkIlRlI9bnEDLypqkIA34zGJmqIS76uKQhAVzXHE55vDlroqkXOoXg==", "8165f8ba-c872-4066-80e7-5f094b736197" });

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(2021, 5, 28, 14, 14, 54, 21, DateTimeKind.Local).AddTicks(9802));

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedAt",
                value: new DateTime(2021, 5, 28, 14, 14, 54, 22, DateTimeKind.Local).AddTicks(211));

            migrationBuilder.UpdateData(
                table: "Editions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(2021, 5, 28, 14, 14, 54, 19, DateTimeKind.Local).AddTicks(3937));

            migrationBuilder.UpdateData(
                table: "Editions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedAt",
                value: new DateTime(2021, 5, 28, 14, 14, 54, 21, DateTimeKind.Local).AddTicks(8993));

            migrationBuilder.UpdateData(
                table: "Editions",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedAt",
                value: new DateTime(2021, 5, 28, 14, 14, 54, 21, DateTimeKind.Local).AddTicks(9028));

            migrationBuilder.UpdateData(
                table: "Editions",
                keyColumn: "Id",
                keyValue: 4L,
                column: "CreatedAt",
                value: new DateTime(2021, 5, 28, 14, 14, 54, 21, DateTimeKind.Local).AddTicks(9034));

            migrationBuilder.UpdateData(
                table: "Editions",
                keyColumn: "Id",
                keyValue: 5L,
                column: "CreatedAt",
                value: new DateTime(2021, 5, 28, 14, 14, 54, 21, DateTimeKind.Local).AddTicks(9037));
        }
    }
}
