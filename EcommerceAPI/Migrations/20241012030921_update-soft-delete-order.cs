using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceAPI.Migrations
{
    public partial class updatesoftdeleteorder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Order",
                type: "bit",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "Password", "UpdatedOn" },
                values: new object[] { new DateTime(2024, 10, 12, 10, 9, 21, 274, DateTimeKind.Local).AddTicks(8521), "AOF971BpBu9nnNC0rNpSg9Y7LRKLElySJOGc6/YWaOhWJw1n", new DateTime(2024, 10, 12, 10, 9, 21, 274, DateTimeKind.Local).AddTicks(8534) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedOn", "Password", "UpdatedOn" },
                values: new object[] { new DateTime(2024, 10, 12, 10, 9, 21, 285, DateTimeKind.Local).AddTicks(7601), "VBTY1vpJ37bPiLaB7nKZQPK/kOUzIrsUaM0IHE4wQuhnXQKe", new DateTime(2024, 10, 12, 10, 9, 21, 285, DateTimeKind.Local).AddTicks(7610) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Order");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "Password", "UpdatedOn" },
                values: new object[] { new DateTime(2024, 9, 5, 21, 26, 0, 401, DateTimeKind.Local).AddTicks(222), "uh5O5TLP1V2hBN/PbjnU7IzvYOGKTORio56WBHmtPTUj35E6", new DateTime(2024, 9, 5, 21, 26, 0, 401, DateTimeKind.Local).AddTicks(236) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedOn", "Password", "UpdatedOn" },
                values: new object[] { new DateTime(2024, 9, 5, 21, 26, 0, 412, DateTimeKind.Local).AddTicks(1886), "0ea1anjQUbBKCLe/+uNt72I5uS+Y5v+LaLuRo+ptamgktYcC", new DateTime(2024, 9, 5, 21, 26, 0, 412, DateTimeKind.Local).AddTicks(1896) });
        }
    }
}
