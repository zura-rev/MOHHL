using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HL.Infrastructure.Persistence.Migrations
{
    public partial class changeperformers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "ResourceUser",
                newName: "ResourceUser",
                newSchema: "dbo");

            migrationBuilder.AlterColumn<DateTime>(
                name: "PerformDate",
                table: "Performers",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "Performers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2021, 8, 5, 13, 11, 47, 490, DateTimeKind.Local).AddTicks(2189));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2021, 8, 5, 13, 11, 47, 491, DateTimeKind.Local).AddTicks(4932));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTime(2021, 8, 5, 13, 11, 47, 491, DateTimeKind.Local).AddTicks(5012));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Note",
                table: "Performers");

            migrationBuilder.RenameTable(
                name: "ResourceUser",
                schema: "dbo",
                newName: "ResourceUser");

            migrationBuilder.AlterColumn<DateTime>(
                name: "PerformDate",
                table: "Performers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2021, 8, 2, 17, 2, 25, 944, DateTimeKind.Local).AddTicks(5016));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2021, 8, 2, 17, 2, 25, 946, DateTimeKind.Local).AddTicks(8844));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTime(2021, 8, 2, 17, 2, 25, 946, DateTimeKind.Local).AddTicks(9017));
        }
    }
}
