using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TiendaIphone.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tienda",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreTienda = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tienda", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Iphones",
                columns: table => new
                {
                    IphoneID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Modelo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Precio = table.Column<int>(type: "int", nullable: false),
                    EstadoIphone = table.Column<int>(type: "int", nullable: false),
                    ColorIphone = table.Column<int>(type: "int", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: true),
                    TiendaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Iphones", x => x.IphoneID);
                    table.ForeignKey(
                        name: "FK_Iphones_Tienda_TiendaID",
                        column: x => x.TiendaID,
                        principalTable: "Tienda",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Iphones_TiendaID",
                table: "Iphones",
                column: "TiendaID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Iphones");

            migrationBuilder.DropTable(
                name: "Tienda");
        }
    }
}
