using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Coopera.Migrations
{
    /// <inheritdoc />
    public partial class Inicializar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Jugadores",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jugadores", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Partidas",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MetaComida = table.Column<int>(type: "int", nullable: false),
                    MetaMadera = table.Column<int>(type: "int", nullable: false),
                    MetaPiedra = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Partidas", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Recursos",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Material = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recursos", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Jugadores_Partidas",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JugadorID = table.Column<int>(type: "int", nullable: false),
                    PartidaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jugadores_Partidas", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Jugadores_Partidas_Jugadores_JugadorID",
                        column: x => x.JugadorID,
                        principalTable: "Jugadores",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Jugadores_Partidas_Partidas_PartidaID",
                        column: x => x.PartidaID,
                        principalTable: "Partidas",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Registros_Recursos",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PartidaID = table.Column<int>(type: "int", nullable: false),
                    RecursoID = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Registros_Recursos", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Registros_Recursos_Partidas_PartidaID",
                        column: x => x.PartidaID,
                        principalTable: "Partidas",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Registros_Recursos_Recursos_RecursoID",
                        column: x => x.RecursoID,
                        principalTable: "Recursos",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Jugadores_Partidas_JugadorID",
                table: "Jugadores_Partidas",
                column: "JugadorID");

            migrationBuilder.CreateIndex(
                name: "IX_Jugadores_Partidas_PartidaID",
                table: "Jugadores_Partidas",
                column: "PartidaID");

            migrationBuilder.CreateIndex(
                name: "IX_Registros_Recursos_PartidaID",
                table: "Registros_Recursos",
                column: "PartidaID");

            migrationBuilder.CreateIndex(
                name: "IX_Registros_Recursos_RecursoID",
                table: "Registros_Recursos",
                column: "RecursoID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Jugadores_Partidas");

            migrationBuilder.DropTable(
                name: "Registros_Recursos");

            migrationBuilder.DropTable(
                name: "Jugadores");

            migrationBuilder.DropTable(
                name: "Partidas");

            migrationBuilder.DropTable(
                name: "Recursos");
        }
    }
}
