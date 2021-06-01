using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EdProject.DAL.Migrations
{
    public partial class remove_fields_in_orderItems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "Currency",
                table: "OrderItems");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2355d9b9-ec44-4547-ac23-1a58d746cb70", "AQAAAAEAACcQAAAAEJhis2vz4u/kM4lFNp7GnX/msEa0uVpU3O1mer2K7OU7eh9N+ML3yFcz41poqKWXUg==", "6090a8ad-d485-489c-83df-c5a88fd76de9" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "083d6e9f-704c-4d0b-8cee-ee7da10112d6", "AQAAAAEAACcQAAAAEAUsMdShAqyXlvvDEx3zHKjVj8YfGGuFmS5M0MSHO6k6FwXLt0lUBo7N33p5mElStg==", "9d3be919-7d31-4faf-b476-8f5bf8db9120" });

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(2021, 6, 1, 10, 34, 23, 627, DateTimeKind.Local).AddTicks(8938));

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedAt",
                value: new DateTime(2021, 6, 1, 10, 34, 23, 627, DateTimeKind.Local).AddTicks(9361));

            migrationBuilder.UpdateData(
                table: "Editions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(2021, 6, 1, 10, 34, 23, 625, DateTimeKind.Local).AddTicks(4387));

            migrationBuilder.UpdateData(
                table: "Editions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedAt",
                value: new DateTime(2021, 6, 1, 10, 34, 23, 627, DateTimeKind.Local).AddTicks(8190));

            migrationBuilder.UpdateData(
                table: "Editions",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedAt",
                value: new DateTime(2021, 6, 1, 10, 34, 23, 627, DateTimeKind.Local).AddTicks(8225));

            migrationBuilder.UpdateData(
                table: "Editions",
                keyColumn: "Id",
                keyValue: 4L,
                column: "CreatedAt",
                value: new DateTime(2021, 6, 1, 10, 34, 23, 627, DateTimeKind.Local).AddTicks(8230));

            migrationBuilder.UpdateData(
                table: "Editions",
                keyColumn: "Id",
                keyValue: 5L,
                column: "CreatedAt",
                value: new DateTime(2021, 6, 1, 10, 34, 23, 627, DateTimeKind.Local).AddTicks(8234));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Amount",
                table: "OrderItems",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0.1m);

            migrationBuilder.AddColumn<int>(
                name: "Currency",
                table: "OrderItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
                column: "CreatedAt",
                value: new DateTime(2021, 5, 31, 11, 51, 42, 940, DateTimeKind.Local).AddTicks(8784));

            migrationBuilder.UpdateData(
                table: "Editions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedAt",
                value: new DateTime(2021, 5, 31, 11, 51, 42, 943, DateTimeKind.Local).AddTicks(3420));

            migrationBuilder.UpdateData(
                table: "Editions",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedAt",
                value: new DateTime(2021, 5, 31, 11, 51, 42, 943, DateTimeKind.Local).AddTicks(3455));

            migrationBuilder.UpdateData(
                table: "Editions",
                keyColumn: "Id",
                keyValue: 4L,
                column: "CreatedAt",
                value: new DateTime(2021, 5, 31, 11, 51, 42, 943, DateTimeKind.Local).AddTicks(3461));

            migrationBuilder.UpdateData(
                table: "Editions",
                keyColumn: "Id",
                keyValue: 5L,
                column: "CreatedAt",
                value: new DateTime(2021, 5, 31, 11, 51, 42, 943, DateTimeKind.Local).AddTicks(3464));
        }
    }
}
