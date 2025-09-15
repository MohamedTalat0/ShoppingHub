using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoppingHub.DAL.Migrations
{
    /// <inheritdoc />
    public partial class CategoryTableCreationandProductRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CATID",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CATID",
                table: "Products",
                column: "CATID");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Category_CATID",
                table: "Products",
                column: "CATID",
                principalTable: "Category",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Category_CATID",
                table: "Products");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropIndex(
                name: "IX_Products_CATID",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CATID",
                table: "Products");
        }
    }
}
