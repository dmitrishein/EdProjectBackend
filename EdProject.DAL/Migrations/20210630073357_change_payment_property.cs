using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EdProject.DAL.Migrations
{
    public partial class change_payment_property : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "Payments",
                type: "decimal(18,4)",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5d8abea0-d06b-4929-9807-877c821f63a5", "AQAAAAEAACcQAAAAEFe23BNJ1QKxNhDhpC9P42N5koKpEeQEyHSfMa5YbZZ5DcMhvfocA+3m6Bungx377w==", "ef33ddf0-0bd5-4c42-8b0c-cccc1d41d82f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5b2506ee-150b-4573-8124-eb21c2239807", "AQAAAAEAACcQAAAAELq/fmQRO95hP4diImRGnY43hRmNAxB66k3p64r9svdc8reE1yHUCTtDTq3RWD91pQ==", "f36efb4e-f0a8-4bbb-bf51-df08c0a2ca54" });

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(2021, 6, 30, 10, 33, 56, 166, DateTimeKind.Local).AddTicks(7797));

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedAt",
                value: new DateTime(2021, 6, 30, 10, 33, 56, 166, DateTimeKind.Local).AddTicks(8082));

            migrationBuilder.UpdateData(
                table: "Editions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(2021, 6, 30, 10, 33, 56, 164, DateTimeKind.Local).AddTicks(3497));

            migrationBuilder.UpdateData(
                table: "Editions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedAt",
                value: new DateTime(2021, 6, 30, 10, 33, 56, 166, DateTimeKind.Local).AddTicks(7146));

            migrationBuilder.UpdateData(
                table: "Editions",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedAt",
                value: new DateTime(2021, 6, 30, 10, 33, 56, 166, DateTimeKind.Local).AddTicks(7180));

            migrationBuilder.UpdateData(
                table: "Editions",
                keyColumn: "Id",
                keyValue: 4L,
                column: "CreatedAt",
                value: new DateTime(2021, 6, 30, 10, 33, 56, 166, DateTimeKind.Local).AddTicks(7185));

            migrationBuilder.UpdateData(
                table: "Editions",
                keyColumn: "Id",
                keyValue: 5L,
                column: "CreatedAt",
                value: new DateTime(2021, 6, 30, 10, 33, 56, 166, DateTimeKind.Local).AddTicks(7188));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "Amount",
                table: "Payments",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)");

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
    }
}
