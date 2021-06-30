using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EdProject.DAL.Migrations
{
    public partial class Total_property_for_orders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Total",
                table: "Orders",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6fe103e6-5481-4af0-972e-23789f07f5dc", "AQAAAAEAACcQAAAAEAxWy7/hCVwGdEN56dpw5MtwtqlIXCLocqs7uMk7HO9TTQ/RUD1UwSDK4evuYKUkiA==", "f6f0ad32-0b7e-4044-93ee-b5bbdbb4e124" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b1089c7c-29f9-46d6-887f-cc42a01c9155", "AQAAAAEAACcQAAAAELXmtq2HqYNMsuAuPgkfzLD7wenvm5Yzd7WRxW+VTXnJLMT+ZBeS9b7T6XlVtBiqnQ==", "e4c0ad9e-ce4f-42ce-9a1a-2890a5e1941e" });

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(2021, 6, 30, 11, 38, 18, 354, DateTimeKind.Local).AddTicks(3830));

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedAt",
                value: new DateTime(2021, 6, 30, 11, 38, 18, 354, DateTimeKind.Local).AddTicks(4128));

            migrationBuilder.UpdateData(
                table: "Editions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(2021, 6, 30, 11, 38, 18, 352, DateTimeKind.Local).AddTicks(47));

            migrationBuilder.UpdateData(
                table: "Editions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedAt",
                value: new DateTime(2021, 6, 30, 11, 38, 18, 354, DateTimeKind.Local).AddTicks(3182));

            migrationBuilder.UpdateData(
                table: "Editions",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedAt",
                value: new DateTime(2021, 6, 30, 11, 38, 18, 354, DateTimeKind.Local).AddTicks(3220));

            migrationBuilder.UpdateData(
                table: "Editions",
                keyColumn: "Id",
                keyValue: 4L,
                column: "CreatedAt",
                value: new DateTime(2021, 6, 30, 11, 38, 18, 354, DateTimeKind.Local).AddTicks(3228));

            migrationBuilder.UpdateData(
                table: "Editions",
                keyColumn: "Id",
                keyValue: 5L,
                column: "CreatedAt",
                value: new DateTime(2021, 6, 30, 11, 38, 18, 354, DateTimeKind.Local).AddTicks(3233));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Total",
                table: "Orders");

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
    }
}
