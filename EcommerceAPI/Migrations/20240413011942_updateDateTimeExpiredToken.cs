using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceAPI.Migrations
{
    public partial class updateDateTimeExpiredToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "RefreshTokenExpiryTime",
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
                columns: new[] { "CreatedOn", "Password", "Role", "UpdatedOn" },
                values: new object[] { new DateTime(2024, 4, 13, 8, 19, 41, 969, DateTimeKind.Local).AddTicks(9029), "vZTtbs01mHL6Qd5Wy2VmREaS+nEhnemnq0nn9TQTMgNeZsKV", 0, new DateTime(2024, 4, 13, 8, 19, 41, 969, DateTimeKind.Local).AddTicks(9031) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "RefreshTokenExpiryTime",
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
                values: new object[] { new DateTime(2024, 3, 31, 13, 34, 52, 191, DateTimeKind.Local).AddTicks(598), "lb7IZuZ+iFZ/XEaLgrttZgqepPUEHZguQLH0f0+N89K1jJuy", new DateTime(2024, 3, 31, 13, 34, 52, 191, DateTimeKind.Local).AddTicks(606) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedOn", "Password", "Role", "UpdatedOn" },
                values: new object[] { new DateTime(2024, 3, 31, 13, 34, 52, 205, DateTimeKind.Local).AddTicks(3893), "wXOyX0s9MfpWuh54eI0kFlQKZxYBi01JIRD1s57l5TpwOYrI", 1, new DateTime(2024, 3, 31, 13, 34, 52, 205, DateTimeKind.Local).AddTicks(3901) });
        }
    }
}
