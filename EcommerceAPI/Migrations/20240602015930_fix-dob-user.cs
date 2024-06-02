using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceAPI.Migrations
{
    public partial class fixdobuser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfBirth",
                table: "User",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "Password", "UpdatedOn" },
                values: new object[] { new DateTime(2024, 6, 2, 8, 59, 29, 912, DateTimeKind.Local).AddTicks(8409), "ATPH83ELNPgRyrQ5qMsVvIpuBXTSxQLsK/yV6OgX4xX4w39k", new DateTime(2024, 6, 2, 8, 59, 29, 912, DateTimeKind.Local).AddTicks(8421) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedOn", "Password", "UpdatedOn" },
                values: new object[] { new DateTime(2024, 6, 2, 8, 59, 29, 925, DateTimeKind.Local).AddTicks(2823), "DVCij4cIaxXJHP9EHtF69qPKLLj3R8/yjuKvAw+Cjd7jWTVS", new DateTime(2024, 6, 2, 8, 59, 29, 925, DateTimeKind.Local).AddTicks(2834) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfBirth",
                table: "User",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "Password", "UpdatedOn" },
                values: new object[] { new DateTime(2024, 4, 13, 8, 19, 41, 961, DateTimeKind.Local).AddTicks(6006), "XSXkvzBVW/DghK2s019J06yaoHveNWqF9uze4S5JUeSzfGpT", new DateTime(2024, 4, 13, 8, 19, 41, 961, DateTimeKind.Local).AddTicks(6015) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedOn", "Password", "UpdatedOn" },
                values: new object[] { new DateTime(2024, 4, 13, 8, 19, 41, 969, DateTimeKind.Local).AddTicks(9029), "vZTtbs01mHL6Qd5Wy2VmREaS+nEhnemnq0nn9TQTMgNeZsKV", new DateTime(2024, 4, 13, 8, 19, 41, 969, DateTimeKind.Local).AddTicks(9031) });
        }
    }
}
