using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class CreatingMovieCastTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MovieCast",
                columns: table => new
                {
                    MovieId = table.Column<int>(type: "int", nullable: false),
                    CastId = table.Column<int>(type: "int", nullable: false),
                    Character = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MovieCastCastId = table.Column<int>(type: "int", nullable: true),
                    MovieCastCharacter = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    MovieCastMovieId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieCast", x => new { x.CastId, x.MovieId, x.Character });
                    table.ForeignKey(
                        name: "FK_MovieCast_Cast_CastId",
                        column: x => x.CastId,
                        principalTable: "Cast",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieCast_Movie_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieCast_MovieCast_MovieCastCastId_MovieCastMovieId_MovieCastCharacter",
                        columns: x => new { x.MovieCastCastId, x.MovieCastMovieId, x.MovieCastCharacter },
                        principalTable: "MovieCast",
                        principalColumns: new[] { "CastId", "MovieId", "Character" });
                });

            migrationBuilder.CreateIndex(
                name: "IX_MovieCast_MovieCastCastId_MovieCastMovieId_MovieCastCharacter",
                table: "MovieCast",
                columns: new[] { "MovieCastCastId", "MovieCastMovieId", "MovieCastCharacter" });

            migrationBuilder.CreateIndex(
                name: "IX_MovieCast_MovieId",
                table: "MovieCast",
                column: "MovieId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovieCast");
        }
    }
}
