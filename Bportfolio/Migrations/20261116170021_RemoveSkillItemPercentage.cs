using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bportfolio.Migrations
{
    /// <inheritdoc />
    public partial class RemoveSkillItemPercentage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Percentage",
                table: "SkillItems");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Percentage",
                table: "SkillItems",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
