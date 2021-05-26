using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EdProject.DAL.Migrations
{
    public partial class remove_custom_role : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RolesType",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "isRemoved",
                table: "AspNetRoles");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "ConcurrencyStamp",
                value: "1");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "ConcurrencyStamp",
                value: "2");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RolesType",
                table: "AspNetRoles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "isRemoved",
                table: "AspNetRoles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ConcurrencyStamp", "RolesType" },
                values: new object[] { "07889453-f641-4efd-8d64-b8f94d117839", 1 });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "ConcurrencyStamp", "RolesType" },
                values: new object[] { "b1b1e1b4-4eb0-438a-8f60-33e00e6c0dc4", 2 });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fb8d32e8-f349-48d6-802f-3d0304b4c491", "AQAAAAEAACcQAAAAEM5UfrMTbnM9+A8gfX/mpv+sVM9UuCkDUHBgU5tnwAaJAgKY5q7sM8ymGpA0KXyPOA==", "54be1e01-80b3-4a8a-843f-149d8dc55dd0" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7165d773-0eab-44eb-9d18-6a8a73458e97", "AQAAAAEAACcQAAAAEOUKTcDmEC9Zh7wx+T/lOI7KeoeOrssIEUOl2OLKEviVA9MusuZjFkl2b5otfEwtVQ==", "a4af2348-450f-4622-8816-33a8baccd46f" });

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(2021, 5, 26, 15, 20, 14, 626, DateTimeKind.Local).AddTicks(4092));

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedAt",
                value: new DateTime(2021, 5, 26, 15, 20, 14, 626, DateTimeKind.Local).AddTicks(4514));

            migrationBuilder.UpdateData(
                table: "Editions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(2021, 5, 26, 15, 20, 14, 623, DateTimeKind.Local).AddTicks(8789));

            migrationBuilder.UpdateData(
                table: "Editions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedAt",
                value: new DateTime(2021, 5, 26, 15, 20, 14, 626, DateTimeKind.Local).AddTicks(3323));

            migrationBuilder.UpdateData(
                table: "Editions",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedAt",
                value: new DateTime(2021, 5, 26, 15, 20, 14, 626, DateTimeKind.Local).AddTicks(3357));

            migrationBuilder.UpdateData(
                table: "Editions",
                keyColumn: "Id",
                keyValue: 4L,
                column: "CreatedAt",
                value: new DateTime(2021, 5, 26, 15, 20, 14, 626, DateTimeKind.Local).AddTicks(3364));

            migrationBuilder.UpdateData(
                table: "Editions",
                keyColumn: "Id",
                keyValue: 5L,
                column: "CreatedAt",
                value: new DateTime(2021, 5, 26, 15, 20, 14, 626, DateTimeKind.Local).AddTicks(3367));
        }
    }
}
