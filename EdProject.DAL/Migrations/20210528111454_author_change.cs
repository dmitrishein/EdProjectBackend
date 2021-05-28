using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EdProject.DAL.Migrations
{
    public partial class author_change : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fc39a822-9a40-4b0e-8465-e20bfd91be80", "AQAAAAEAACcQAAAAED2tdilraxWUUKgd1+pFiaNPL6RbaY0j5MUg5vBk39vwwPKqhRsaMQ9MwavJdkBU/Q==", "88edd25b-b068-416f-9c46-79724c20539a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "85405c37-d33d-4826-85d3-579ff0a0d5d1", "AQAAAAEAACcQAAAAENn12DWd/88Tg+XErHNjmCt5oAjeSVnxKfZDqf7kEOMgsCMwCxZSy6bngmTgNyZpFw==", "478955b1-6a4c-4eaa-b528-36ce300084e6" });

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(2021, 5, 26, 15, 42, 8, 464, DateTimeKind.Local).AddTicks(5655));

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedAt",
                value: new DateTime(2021, 5, 26, 15, 42, 8, 464, DateTimeKind.Local).AddTicks(6292));

            migrationBuilder.UpdateData(
                table: "Editions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(2021, 5, 26, 15, 42, 8, 461, DateTimeKind.Local).AddTicks(5665));

            migrationBuilder.UpdateData(
                table: "Editions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedAt",
                value: new DateTime(2021, 5, 26, 15, 42, 8, 464, DateTimeKind.Local).AddTicks(4143));

            migrationBuilder.UpdateData(
                table: "Editions",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedAt",
                value: new DateTime(2021, 5, 26, 15, 42, 8, 464, DateTimeKind.Local).AddTicks(4234));

            migrationBuilder.UpdateData(
                table: "Editions",
                keyColumn: "Id",
                keyValue: 4L,
                column: "CreatedAt",
                value: new DateTime(2021, 5, 26, 15, 42, 8, 464, DateTimeKind.Local).AddTicks(4240));

            migrationBuilder.UpdateData(
                table: "Editions",
                keyColumn: "Id",
                keyValue: 5L,
                column: "CreatedAt",
                value: new DateTime(2021, 5, 26, 15, 42, 8, 464, DateTimeKind.Local).AddTicks(4312));
        }
    }
}
