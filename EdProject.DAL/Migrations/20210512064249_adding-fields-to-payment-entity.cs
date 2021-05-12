using Microsoft.EntityFrameworkCore.Migrations;

namespace EdProject.DAL.Migrations
{
    public partial class addingfieldstopaymententity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Amount",
                table: "Payments",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "Currency",
                table: "Payments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "ConcurrencyStamp",
                value: "bfb6979f-a4b6-4eb9-9e00-d0d7b7f2f5b3");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "ConcurrencyStamp",
                value: "86767788-1477-4e26-aea1-eaa3133e4492");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0742521f-81bb-42a1-afa9-979193460856", "AQAAAAEAACcQAAAAEG8DGH5G8uwm8p2nghnROmErPka139HnEyMFasIAISDUT+SLxnQQN+rpRIYF0E/WrQ==", "e0fbaa0b-8b0a-4963-bbb4-80b46f8daadc" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f19d33a4-d92c-44b5-be83-0054964df430", "AQAAAAEAACcQAAAAEBhy0e/iY4DcTAJE36sR417P8Fk/wr+WxIvM528haBNVy8b9SlniLp8dqrsG+cyjZg==", "4635b590-c839-4b59-b892-1b933b8c029d" });

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedAt",
                value: "2021-05-12 09:42:48Z");

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedAt",
                value: "2021-05-12 09:42:48Z");

            migrationBuilder.UpdateData(
                table: "Editions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedAt",
                value: "2021-05-12 09:42:48Z");

            migrationBuilder.UpdateData(
                table: "Editions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedAt",
                value: "2021-05-12 09:42:48Z");

            migrationBuilder.UpdateData(
                table: "Editions",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedAt",
                value: "2021-05-12 09:42:48Z");

            migrationBuilder.UpdateData(
                table: "Editions",
                keyColumn: "Id",
                keyValue: 4L,
                column: "CreatedAt",
                value: "2021-05-12 09:42:48Z");

            migrationBuilder.UpdateData(
                table: "Editions",
                keyColumn: "Id",
                keyValue: 5L,
                column: "CreatedAt",
                value: "2021-05-12 09:42:48Z");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "Currency",
                table: "Payments");

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
    }
}
