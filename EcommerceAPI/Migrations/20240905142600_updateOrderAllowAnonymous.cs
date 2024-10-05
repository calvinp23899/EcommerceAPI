using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceAPI.Migrations
{
    public partial class updateOrderAllowAnonymous : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "ProductFile",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Order",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "AnonymousAddress",
                table: "Order",
                type: "varchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AnonymousEmail",
                table: "Order",
                type: "varchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AnonymousName",
                table: "Order",
                type: "varchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AnonymousPhone",
                table: "Order",
                type: "varchar(255)",
                maxLength: 255,
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnonymousAddress",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "AnonymousEmail",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "AnonymousName",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "AnonymousPhone",
                table: "Order");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "ProductFile",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Order",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "Password", "UpdatedOn" },
                values: new object[] { new DateTime(2024, 7, 3, 21, 29, 25, 614, DateTimeKind.Local).AddTicks(3012), "kSeGNku8Mbtc4r+0b5l9IFl8OWCUBFngA/qeCThKLzLW/1RV", new DateTime(2024, 7, 3, 21, 29, 25, 614, DateTimeKind.Local).AddTicks(3025) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedOn", "Password", "UpdatedOn" },
                values: new object[] { new DateTime(2024, 7, 3, 21, 29, 25, 623, DateTimeKind.Local).AddTicks(6415), "mK6Yl16rHZo70mnsgiYZtDNN0JjPcmqQlZJMu+FQytIKDP0V", new DateTime(2024, 7, 3, 21, 29, 25, 623, DateTimeKind.Local).AddTicks(6418) });
        }
    }
}
