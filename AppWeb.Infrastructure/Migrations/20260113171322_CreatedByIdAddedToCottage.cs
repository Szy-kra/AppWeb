using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppWeb.Infrastructure.Migrations
{
    public partial class CreatedByIdAddedToCottage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Dodanie kolumny do tabeli Cottages
            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "Cottages",
                type: "nvarchar(450)",
                nullable: true);

            // Stworzenie indeksu dla nowej kolumny
            migrationBuilder.CreateIndex(
                name: "IX_Cottages_CreatedById",
                table: "Cottages",
                column: "CreatedById");

            // Powiązanie kolumny z tabelą użytkowników (Klucz Obcy)
            migrationBuilder.AddForeignKey(
                name: "FK_Cottages_AspNetUsers_CreatedById",
                table: "Cottages",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cottages_AspNetUsers_CreatedById",
                table: "Cottages");

            migrationBuilder.DropIndex(
                name: "IX_Cottages_CreatedById",
                table: "Cottages");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Cottages");
        }
    }
}