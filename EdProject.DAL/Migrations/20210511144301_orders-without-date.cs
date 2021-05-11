using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EdProject.DAL.Migrations
{
    public partial class orderswithoutdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "Orders");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "ConcurrencyStamp",
                value: "4a9d2a25-026a-4fa6-9e5a-7360b52b20b4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "ConcurrencyStamp",
                value: "00eaf7ef-d4dd-4373-9ac9-6800a4285f41");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ccb28cdb-6379-40c5-9b77-a5a7ab44ced4", "AQAAAAEAACcQAAAAEJ0VKWwOmHVc16tGQgLr2mihAKLdyw4WOQ2Fb9L+YJaEbpZ8VkcLkZ2HqfALvxeuKQ==", "a2914044-e28a-46e7-ad9a-07a46a08fb58" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "54fe6409-8fbd-482f-b067-5f5f28602d05", "AQAAAAEAACcQAAAAEKR7YFJyfpGPMWlbzyARsPjpD+vmVblREd0+DoBI+lOoQCv+H4l1MKI5P0L7o3Hvww==", "cea3c488-74a2-4a44-a712-641a8862f9f4" });

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedAt",
                value: "2021-05-11 17:43:00Z");

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedAt",
                value: "2021-05-11 17:43:00Z");

            migrationBuilder.UpdateData(
                table: "Editions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedAt",
                value: "2021-05-11 17:43:00Z");

            migrationBuilder.UpdateData(
                table: "Editions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedAt",
                value: "2021-05-11 17:43:00Z");

            migrationBuilder.UpdateData(
                table: "Editions",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedAt",
                value: "2021-05-11 17:43:00Z");

            migrationBuilder.UpdateData(
                table: "Editions",
                keyColumn: "Id",
                keyValue: 4L,
                column: "CreatedAt",
                value: "2021-05-11 17:43:00Z");

            migrationBuilder.UpdateData(
                table: "Editions",
                keyColumn: "Id",
                keyValue: 5L,
                column: "CreatedAt",
                value: "2021-05-11 17:43:00Z");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "ConcurrencyStamp",
                value: "74146a6f-77ea-4b3a-8e88-bfa96f8f0cf1");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "ConcurrencyStamp",
                value: "f50b57a1-617c-4d57-8dba-e0bbd93b1f91");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bd01a517-2176-4d01-b13a-7b9c41d187dc", "AQAAAAEAACcQAAAAEED/zAVUHBmCF4yKuMTzPk23MNfzjoyo36eh40aDiHxhPxytIlXMUVuKQRx/oe7gxQ==", "c80d1c5a-2cb0-418c-87f3-51e620f9335c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "277ee3a1-0f78-421f-bcdd-7b6013732525", "AQAAAAEAACcQAAAAENRq8BhvJkhAWqPIkSXYrE3zoBy2cA7JI2dnzMjVFeO3yTElZe//QQbU8NmG5NNRFw==", "3dfeae9c-077d-48bb-94d7-ec5b6b7b3e41" });

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedAt",
                value: "2021-05-10 15:47:33Z");

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedAt",
                value: "2021-05-10 15:47:33Z");

            migrationBuilder.UpdateData(
                table: "Editions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedAt",
                value: "2021-05-10 15:47:33Z");

            migrationBuilder.UpdateData(
                table: "Editions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedAt",
                value: "2021-05-10 15:47:33Z");

            migrationBuilder.UpdateData(
                table: "Editions",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedAt",
                value: "2021-05-10 15:47:33Z");

            migrationBuilder.UpdateData(
                table: "Editions",
                keyColumn: "Id",
                keyValue: 4L,
                column: "CreatedAt",
                value: "2021-05-10 15:47:33Z");

            migrationBuilder.UpdateData(
                table: "Editions",
                keyColumn: "Id",
                keyValue: 5L,
                column: "CreatedAt",
                value: "2021-05-10 15:47:33Z");
        }
    }
}
