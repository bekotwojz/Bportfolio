using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bportfolio.Migrations
{
    /// <inheritdoc />
    public partial class AddSkillItemPercentage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Percentage",
                table: "SkillItems",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Percentage",
                table: "SkillItems");
        }
    }
}
