using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppWeb.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CottageAboutAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "About",
                table: "Cottages",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "About",
                table: "Cottages");
        }
    }
}
