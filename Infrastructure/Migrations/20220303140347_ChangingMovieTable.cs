using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class ChangingMovieTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Revenue",
                table: "Movie",
                type: "decimal(18,2)",
                nullable: true,
                defaultValue: 9.9m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldNullable: true,
                oldDefaultValue: 9.9m);

            migrationBuilder.AlterColumn<decimal>(
                name: "Budget",
                table: "Movie",
                type: "decimal(18,2)",
                nullable: true,
                defaultValue: 9.9m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldNullable: true,
                oldDefaultValue: 9.9m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Revenue",
                table: "Movie",
                type: "decimal(18,4)",
                nullable: true,
                defaultValue: 9.9m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true,
                oldDefaultValue: 9.9m);

            migrationBuilder.AlterColumn<decimal>(
                name: "Budget",
                table: "Movie",
                type: "decimal(18,4)",
                nullable: true,
                defaultValue: 9.9m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true,
                oldDefaultValue: 9.9m);
        }
    }
}
