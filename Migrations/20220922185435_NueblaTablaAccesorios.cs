using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TiendaIphone.Migrations
{
    public partial class NueblaTablaAccesorios : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accesorios",
                columns: table => new
                {
                    AccesorioID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Modelo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Precio = table.Column<int>(type: "int", nullable: false),
                    EstadoAccesorio = table.Column<int>(type: "int", nullable: false),
                    ColorAccesorio = table.Column<int>(type: "int", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: true),
                    TiendaAccesorioID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accesorios", x => x.AccesorioID);
                    table.ForeignKey(
                        name: "FK_Accesorios_Tienda_TiendaAccesorioID",
                        column: x => x.TiendaAccesorioID,
                        principalTable: "Tienda",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accesorios_TiendaAccesorioID",
                table: "Accesorios",
                column: "TiendaAccesorioID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accesorios");
        }
    }
}
