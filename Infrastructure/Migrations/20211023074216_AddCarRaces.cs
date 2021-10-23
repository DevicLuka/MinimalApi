using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class AddCarRaces : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CarRaceId",
                table: "Cars",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CarRaces",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Distance = table.Column<int>(type: "int", nullable: false),
                    TimeLimit = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarRaces", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cars_CarRaceId",
                table: "Cars",
                column: "CarRaceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_CarRaces_CarRaceId",
                table: "Cars",
                column: "CarRaceId",
                principalTable: "CarRaces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_CarRaces_CarRaceId",
                table: "Cars");

            migrationBuilder.DropTable(
                name: "CarRaces");

            migrationBuilder.DropIndex(
                name: "IX_Cars_CarRaceId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "CarRaceId",
                table: "Cars");
        }
    }
}
