using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mocan_Melisa_MariaMVC.Migrations
{
    /// <inheritdoc />
    public partial class PetModelUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PetName",
                table: "Pet",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PetName",
                table: "Pet");
        }
    }
}
