using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EdProject.DAL.Migrations
{
    public partial class addrefresh_token_to_user : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "RefreshTokenExpiryTime",
                table: "AspNetUsers",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a660cb48-860c-4674-acd6-7fd1834dd859", "AQAAAAEAACcQAAAAENXOyZ2pJVSwXAdoHX7J8I3027clBmlek6aQy0QzmO5qOBA4JmP/XrbcyvysXAXLUg==", "f6c0af28-4d58-493b-b81e-1921d0b20e5b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "660b8cf8-1fc3-40e5-98e5-ec0500760f95", "AQAAAAEAACcQAAAAEPFn9TOzRj6tc0g3N+uK3j0Gn0eVwJ8riMkaDAVAPzmDh/r1J5W77C+5g4PNlQs/Bw==", "022c9f95-c3f1-47ee-b82a-0d80ebd24d8b" });

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(2021, 6, 8, 9, 57, 49, 461, DateTimeKind.Local).AddTicks(82));

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedAt",
                value: new DateTime(2021, 6, 8, 9, 57, 49, 461, DateTimeKind.Local).AddTicks(353));

            migrationBuilder.UpdateData(
                table: "Editions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(2021, 6, 8, 9, 57, 49, 458, DateTimeKind.Local).AddTicks(1924));

            migrationBuilder.UpdateData(
                table: "Editions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedAt",
                value: new DateTime(2021, 6, 8, 9, 57, 49, 460, DateTimeKind.Local).AddTicks(9471));

            migrationBuilder.UpdateData(
                table: "Editions",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedAt",
                value: new DateTime(2021, 6, 8, 9, 57, 49, 460, DateTimeKind.Local).AddTicks(9503));

            migrationBuilder.UpdateData(
                table: "Editions",
                keyColumn: "Id",
                keyValue: 4L,
                column: "CreatedAt",
                value: new DateTime(2021, 6, 8, 9, 57, 49, 460, DateTimeKind.Local).AddTicks(9509));

            migrationBuilder.UpdateData(
                table: "Editions",
                keyColumn: "Id",
                keyValue: 5L,
                column: "CreatedAt",
                value: new DateTime(2021, 6, 8, 9, 57, 49, 460, DateTimeKind.Local).AddTicks(9513));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiryTime",
                table: "AspNetUsers");

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
    }
}
