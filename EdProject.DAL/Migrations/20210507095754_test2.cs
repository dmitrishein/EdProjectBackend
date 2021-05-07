using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EdProject.DAL.Migrations
{
    public partial class test2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Editions_EditionId",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_EditionId",
                table: "OrderItems");

            migrationBuilder.CreateTable(
                name: "EditionOrderItems",
                columns: table => new
                {
                    EditionId = table.Column<long>(type: "bigint", nullable: false),
                    OrderItemId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EditionOrderItems", x => new { x.EditionId, x.OrderItemId });
                    table.ForeignKey(
                        name: "FK_EditionOrderItems_Editions_EditionId",
                        column: x => x.EditionId,
                        principalTable: "Editions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EditionOrderItems_OrderItems_OrderItemId",
                        column: x => x.OrderItemId,
                        principalTable: "OrderItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "ConcurrencyStamp",
                value: "c7c64ed8-404b-4cd4-8539-eaf19d4406b3");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "ConcurrencyStamp",
                value: "ced37510-a790-44b8-ae28-3d6f40661749");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a4944149-577e-4df5-9834-ccf8c2c17c2d", "AQAAAAEAACcQAAAAEIKCEhthLNEN0wdkUqPqLXX43/d7qXMHFG7R7mWrvTs5PIAY/NUJATpP7sqsNygDwQ==", "cbb168bf-1355-4edd-8f5b-a0cddfda1558" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d5258949-a7d0-4f32-874f-8e5665fdbb2d", "AQAAAAEAACcQAAAAECi+grLQhsTQdRgQj5H+eKmOKnjS6sYVqYFWmmvUTVOpcemteGkUqADfNba/a7+Gcw==", "1513d352-5544-4dd5-b274-ba85a02243c8" });

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(2021, 5, 7, 12, 57, 53, 860, DateTimeKind.Local).AddTicks(5513));

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedAt",
                value: new DateTime(2021, 5, 7, 12, 57, 53, 862, DateTimeKind.Local).AddTicks(8402));

            migrationBuilder.UpdateData(
                table: "Editions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(2021, 5, 7, 12, 57, 53, 862, DateTimeKind.Local).AddTicks(9169));

            migrationBuilder.UpdateData(
                table: "Editions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedAt",
                value: new DateTime(2021, 5, 7, 12, 57, 53, 863, DateTimeKind.Local).AddTicks(1623));

            migrationBuilder.UpdateData(
                table: "Editions",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedAt",
                value: new DateTime(2021, 5, 7, 12, 57, 53, 863, DateTimeKind.Local).AddTicks(1636));

            migrationBuilder.UpdateData(
                table: "Editions",
                keyColumn: "Id",
                keyValue: 4L,
                column: "CreatedAt",
                value: new DateTime(2021, 5, 7, 12, 57, 53, 863, DateTimeKind.Local).AddTicks(1640));

            migrationBuilder.UpdateData(
                table: "Editions",
                keyColumn: "Id",
                keyValue: 5L,
                column: "CreatedAt",
                value: new DateTime(2021, 5, 7, 12, 57, 53, 863, DateTimeKind.Local).AddTicks(1644));

            migrationBuilder.CreateIndex(
                name: "IX_EditionOrderItems_OrderItemId",
                table: "EditionOrderItems",
                column: "OrderItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EditionOrderItems");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "ConcurrencyStamp",
                value: "aaada3e2-f07a-461b-9700-3954a55a99a8");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "ConcurrencyStamp",
                value: "aa1cc5ad-5eb3-4e06-aa9f-9f00c0b53618");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "98dc0692-42c9-4767-87f3-a4618fb0394b", "AQAAAAEAACcQAAAAEGIzYtGd6nifrge1emi6u+8AKj4738ptmalptOXNgy0oPTWWs2USTUaiSi6yCdN4fA==", "45e05340-b8c7-475d-97c4-0d4003d9f684" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8bb4f311-d2eb-472c-bdf9-e3330cde83f4", "AQAAAAEAACcQAAAAEFbnV6kWeqxk1FslUVqcNCRAfFaUEQ+NsaKYuFxV7LvwuQ9lYbRvPEUn0FqWBJPgmg==", "96c2d851-479c-4069-921b-c056c8383f6f" });

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(2021, 4, 27, 12, 2, 9, 283, DateTimeKind.Local).AddTicks(7729));

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedAt",
                value: new DateTime(2021, 4, 27, 12, 2, 9, 286, DateTimeKind.Local).AddTicks(257));

            migrationBuilder.UpdateData(
                table: "Editions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(2021, 4, 27, 12, 2, 9, 286, DateTimeKind.Local).AddTicks(1056));

            migrationBuilder.UpdateData(
                table: "Editions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedAt",
                value: new DateTime(2021, 4, 27, 12, 2, 9, 286, DateTimeKind.Local).AddTicks(3516));

            migrationBuilder.UpdateData(
                table: "Editions",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedAt",
                value: new DateTime(2021, 4, 27, 12, 2, 9, 286, DateTimeKind.Local).AddTicks(3530));

            migrationBuilder.UpdateData(
                table: "Editions",
                keyColumn: "Id",
                keyValue: 4L,
                column: "CreatedAt",
                value: new DateTime(2021, 4, 27, 12, 2, 9, 286, DateTimeKind.Local).AddTicks(3534));

            migrationBuilder.UpdateData(
                table: "Editions",
                keyColumn: "Id",
                keyValue: 5L,
                column: "CreatedAt",
                value: new DateTime(2021, 4, 27, 12, 2, 9, 286, DateTimeKind.Local).AddTicks(3537));

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_EditionId",
                table: "OrderItems",
                column: "EditionId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Editions_EditionId",
                table: "OrderItems",
                column: "EditionId",
                principalTable: "Editions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
