using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CM20314.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Entity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Discriminator = table.Column<string>(type: "TEXT", nullable: false),
                    ShortName = table.Column<string>(type: "TEXT", nullable: true),
                    LongName = table.Column<string>(type: "TEXT", nullable: true),
                    PolylineIds = table.Column<string>(type: "TEXT", nullable: true),
                    Floors = table.Column<string>(type: "TEXT", nullable: true),
                    PrimaryFloor = table.Column<int>(type: "INTEGER", nullable: true),
                    SecondaryFloor = table.Column<int>(type: "INTEGER", nullable: true),
                    Room_BuildingId = table.Column<int>(type: "INTEGER", nullable: true),
                    ExcludeFromRooms = table.Column<bool>(type: "INTEGER", nullable: true),
                    X = table.Column<double>(type: "REAL", nullable: true),
                    Y = table.Column<double>(type: "REAL", nullable: true),
                    Floor = table.Column<int>(type: "INTEGER", nullable: true),
                    CoordinateId = table.Column<int>(type: "INTEGER", nullable: true),
                    BuildingId = table.Column<int>(type: "INTEGER", nullable: true),
                    Node1Id = table.Column<int>(type: "INTEGER", nullable: true),
                    Node2Id = table.Column<int>(type: "INTEGER", nullable: true),
                    StepFree = table.Column<bool>(type: "INTEGER", nullable: true),
                    Cost = table.Column<double>(type: "REAL", nullable: true),
                    NodeArcType = table.Column<int>(type: "INTEGER", nullable: true),
                    RequiresUsageRequest = table.Column<bool>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entity", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Entity");
        }
    }
}
