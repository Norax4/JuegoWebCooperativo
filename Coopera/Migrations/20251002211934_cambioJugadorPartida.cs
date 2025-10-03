using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Coopera.Migrations
{
    /// <inheritdoc />
    public partial class cambioJugadorPartida : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_JugadoresPartidas",
                table: "JugadoresPartidas");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "JugadoresPartidas",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JugadoresPartidas",
                table: "JugadoresPartidas",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_JugadoresPartidas_JugadorId_PartidaId",
                table: "JugadoresPartidas",
                columns: new[] { "JugadorId", "PartidaId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_JugadoresPartidas",
                table: "JugadoresPartidas");

            migrationBuilder.DropIndex(
                name: "IX_JugadoresPartidas_JugadorId_PartidaId",
                table: "JugadoresPartidas");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "JugadoresPartidas");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JugadoresPartidas",
                table: "JugadoresPartidas",
                columns: new[] { "JugadorId", "PartidaId" });
        }
    }
}
