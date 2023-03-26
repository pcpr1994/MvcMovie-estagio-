using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MvcMovie.Migrations
{
    /// <inheritdoc />
    public partial class atmovie : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Countries",
                table: "AppMovies",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IdImdb",
                table: "AppMovies",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Languages",
                table: "AppMovies",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Plot",
                table: "AppMovies",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RuntimeMins",
                table: "AppMovies",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Stars",
                table: "AppMovies",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Writers",
                table: "AppMovies",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Countries",
                table: "AppMovies");

            migrationBuilder.DropColumn(
                name: "IdImdb",
                table: "AppMovies");

            migrationBuilder.DropColumn(
                name: "Languages",
                table: "AppMovies");

            migrationBuilder.DropColumn(
                name: "Plot",
                table: "AppMovies");

            migrationBuilder.DropColumn(
                name: "RuntimeMins",
                table: "AppMovies");

            migrationBuilder.DropColumn(
                name: "Stars",
                table: "AppMovies");

            migrationBuilder.DropColumn(
                name: "Writers",
                table: "AppMovies");
        }
    }
}
