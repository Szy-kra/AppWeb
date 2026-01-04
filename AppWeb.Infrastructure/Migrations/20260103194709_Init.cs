using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppWeb.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cottages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ContactDetails_PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactDetails_Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactDetails_Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactDetails_City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactDetails_PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EncodedName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cottages", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cottages");
        }
    }
}
