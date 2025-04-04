using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dodduongphi2122110567.Migrations
{
    /// <inheritdoc />
    public partial class AddToProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Qty",
                table: "Categories",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Qty",
                table: "Categories");
        }
    }
}
