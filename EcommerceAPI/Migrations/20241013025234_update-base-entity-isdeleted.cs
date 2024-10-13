using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceAPI.Migrations
{
    public partial class updatebaseentityisdeleted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Vendor",
                type: "bit",
                nullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "User",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ProductFile",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Product",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "OrderDetail",
                type: "bit",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "Password", "UpdatedOn" },
                values: new object[] { new DateTime(2024, 10, 13, 9, 52, 33, 737, DateTimeKind.Local).AddTicks(7373), "0iBcVCmNPGuaIaHGgnFxTvjs6k1/MBJM2+RqxeMNZZW+sdfV", new DateTime(2024, 10, 13, 9, 52, 33, 737, DateTimeKind.Local).AddTicks(7383) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedOn", "Password", "UpdatedOn" },
                values: new object[] { new DateTime(2024, 10, 13, 9, 52, 33, 748, DateTimeKind.Local).AddTicks(9251), "5tRDthpqGKyRtC7n75O0oxJsWwEikQDuTdNLs3BItdyAia5L", new DateTime(2024, 10, 13, 9, 52, 33, 748, DateTimeKind.Local).AddTicks(9260) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Vendor");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ProductFile");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "OrderDetail");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "User",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

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
    }
}
