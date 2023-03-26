using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MvcMovie.Migrations
{
    /// <inheritdoc />
    public partial class atmovieNewCamps : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Awards",
                table: "AppMovies",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image1",
                table: "AppMovies",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image2",
                table: "AppMovies",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LinkEmbed",
                table: "AppMovies",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LinkPoster1",
                table: "AppMovies",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LinkPoster2",
                table: "AppMovies",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VideoDescription",
                table: "AppMovies",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Awards",
                table: "AppMovies");

            migrationBuilder.DropColumn(
                name: "Image1",
                table: "AppMovies");

            migrationBuilder.DropColumn(
                name: "Image2",
                table: "AppMovies");

            migrationBuilder.DropColumn(
                name: "LinkEmbed",
                table: "AppMovies");

            migrationBuilder.DropColumn(
                name: "LinkPoster1",
                table: "AppMovies");

            migrationBuilder.DropColumn(
                name: "LinkPoster2",
                table: "AppMovies");

            migrationBuilder.DropColumn(
                name: "VideoDescription",
                table: "AppMovies");
        }
    }
}
