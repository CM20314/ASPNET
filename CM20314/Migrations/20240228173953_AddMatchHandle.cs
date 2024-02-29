using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CM20314.Migrations
{
    public partial class AddMatchHandle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MatchHandle",
                table: "Entity",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MatchHandle",
                table: "Entity");
        }
    }
}
