using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EdProject.DAL.Migrations
{
    public partial class addorderItementity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Editions_EditionsId",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Orders_OrdersId",
                table: "OrderItems");

            migrationBuilder.RenameColumn(
                name: "OrdersId",
                table: "OrderItems",
                newName: "OrderId");

            migrationBuilder.RenameColumn(
                name: "EditionsId",
                table: "OrderItems",
                newName: "EditionId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItems_OrdersId",
                table: "OrderItems",
                newName: "IX_OrderItems_OrderId");

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

            migrationBuilder.AddColumn<int>(
                name: "ItemsCount",
                table: "OrderItems",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "ConcurrencyStamp",
                value: "07889453-f641-4efd-8d64-b8f94d117839");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "ConcurrencyStamp",
                value: "b1b1e1b4-4eb0-438a-8f60-33e00e6c0dc4");

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

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Editions_EditionId",
                table: "OrderItems",
                column: "EditionId",
                principalTable: "Editions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Orders_OrderId",
                table: "OrderItems",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Editions_EditionId",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Orders_OrderId",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "Currency",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "ItemsCount",
                table: "OrderItems");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "OrderItems",
                newName: "OrdersId");

            migrationBuilder.RenameColumn(
                name: "EditionId",
                table: "OrderItems",
                newName: "EditionsId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                newName: "IX_OrderItems_OrdersId");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "ConcurrencyStamp",
                value: "050dbed0-97d0-420b-83a0-e99da26a5cd8");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "ConcurrencyStamp",
                value: "65e297f7-eaba-4314-b29f-792bd4583c7c");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0db809a8-819b-40b3-8982-6d32305e65f6", "AQAAAAEAACcQAAAAEA9QvS4Dw7FgWSmVtC14Bj5oAv8+mPPf7sWctYEDW2KidD/xj3DedypUKWZTzMIMhg==", "8999e4fc-407f-4f33-acfd-0b378c3cc335" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1e3b0eac-00cf-488e-8c66-9879b09e4753", "AQAAAAEAACcQAAAAEOiVcnZOR+sYGMCbuiNK4fCu4avhpSWqGrPcgB2jQLjojd92dXHgS/4CLrRDKOzL+A==", "d4fa6567-b931-41bb-a933-ca6edd24cc1c" });

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(2021, 5, 20, 15, 12, 46, 463, DateTimeKind.Local).AddTicks(4583));

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedAt",
                value: new DateTime(2021, 5, 20, 15, 12, 46, 463, DateTimeKind.Local).AddTicks(4992));

            migrationBuilder.UpdateData(
                table: "Editions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(2021, 5, 20, 15, 12, 46, 460, DateTimeKind.Local).AddTicks(6667));

            migrationBuilder.UpdateData(
                table: "Editions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedAt",
                value: new DateTime(2021, 5, 20, 15, 12, 46, 463, DateTimeKind.Local).AddTicks(3755));

            migrationBuilder.UpdateData(
                table: "Editions",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedAt",
                value: new DateTime(2021, 5, 20, 15, 12, 46, 463, DateTimeKind.Local).AddTicks(3799));

            migrationBuilder.UpdateData(
                table: "Editions",
                keyColumn: "Id",
                keyValue: 4L,
                column: "CreatedAt",
                value: new DateTime(2021, 5, 20, 15, 12, 46, 463, DateTimeKind.Local).AddTicks(3807));

            migrationBuilder.UpdateData(
                table: "Editions",
                keyColumn: "Id",
                keyValue: 5L,
                column: "CreatedAt",
                value: new DateTime(2021, 5, 20, 15, 12, 46, 463, DateTimeKind.Local).AddTicks(3811));

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Editions_EditionsId",
                table: "OrderItems",
                column: "EditionsId",
                principalTable: "Editions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Orders_OrdersId",
                table: "OrderItems",
                column: "OrdersId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
