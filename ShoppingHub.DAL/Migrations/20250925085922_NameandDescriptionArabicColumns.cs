using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoppingHub.DAL.Migrations
{
    /// <inheritdoc />
    public partial class NameandDescriptionArabicColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DescriptionAR",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProductNameAR",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DescriptionAR",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductNameAR",
                table: "Products");
        }
    }
}
