using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EdProject.DAL.Migrations
{
    public partial class test3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EditionOrderItems");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "ConcurrencyStamp",
                value: "5fd2179b-2a9a-4271-8ebe-debddcc05c1a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "ConcurrencyStamp",
                value: "bda8a2d0-c2fd-4873-a666-5adf015b1fa2");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fa8ca22d-9d79-422c-8307-0684dcf0ce32", "AQAAAAEAACcQAAAAEDM7B5vKTAK/44rGJl5uk36briuIwgrVNeE5J87r2Q773OKioawbsqTUH6GgncU/UQ==", "e3802aaf-5048-4a7b-9490-f6a9707bbf21" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "335f1da0-fa9d-418d-a168-a75bc6aa301d", "AQAAAAEAACcQAAAAEFAJVGW3WNdSoth3tEBxdpZ5biWKea8PQ+1rVTMO5pPXB4GSyyok6pVU0NpqLeZPww==", "50ab4bf2-113d-4218-8de4-c3e17203616a" });

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(2021, 5, 7, 13, 17, 59, 912, DateTimeKind.Local).AddTicks(3899));

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedAt",
                value: new DateTime(2021, 5, 7, 13, 17, 59, 915, DateTimeKind.Local).AddTicks(3079));

            migrationBuilder.UpdateData(
                table: "Editions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(2021, 5, 7, 13, 17, 59, 915, DateTimeKind.Local).AddTicks(4307));

            migrationBuilder.UpdateData(
                table: "Editions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedAt",
                value: new DateTime(2021, 5, 7, 13, 17, 59, 915, DateTimeKind.Local).AddTicks(7077));

            migrationBuilder.UpdateData(
                table: "Editions",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedAt",
                value: new DateTime(2021, 5, 7, 13, 17, 59, 915, DateTimeKind.Local).AddTicks(7092));

            migrationBuilder.UpdateData(
                table: "Editions",
                keyColumn: "Id",
                keyValue: 4L,
                column: "CreatedAt",
                value: new DateTime(2021, 5, 7, 13, 17, 59, 915, DateTimeKind.Local).AddTicks(7095));

            migrationBuilder.UpdateData(
                table: "Editions",
                keyColumn: "Id",
                keyValue: 5L,
                column: "CreatedAt",
                value: new DateTime(2021, 5, 7, 13, 17, 59, 915, DateTimeKind.Local).AddTicks(7098));

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_EditionId",
                table: "OrderItems",
                column: "EditionId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Editions_EditionId",
                table: "OrderItems",
                column: "EditionId",
                principalTable: "Editions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
