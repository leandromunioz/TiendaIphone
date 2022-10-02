using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TiendaIphone.Migrations
{
    public partial class iPhoneConNuevosParam : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DisponibilidadIphone",
                table: "Iphones",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaAltaIphone",
                table: "Iphones",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisponibilidadIphone",
                table: "Iphones");

            migrationBuilder.DropColumn(
                name: "FechaAltaIphone",
                table: "Iphones");
        }
    }
}
