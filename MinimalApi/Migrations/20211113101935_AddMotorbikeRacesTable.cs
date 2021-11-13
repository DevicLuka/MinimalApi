using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MinimalApi.Migrations
{
    public partial class AddMotorbikeRacesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MotorbikeRaceId",
                table: "Motorbikes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MotorbikeRaces",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Distance = table.Column<int>(type: "int", nullable: false),
                    TimeLimit = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MotorbikeRaces", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Motorbikes_MotorbikeRaceId",
                table: "Motorbikes",
                column: "MotorbikeRaceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Motorbikes_MotorbikeRaces_MotorbikeRaceId",
                table: "Motorbikes",
                column: "MotorbikeRaceId",
                principalTable: "MotorbikeRaces",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Motorbikes_MotorbikeRaces_MotorbikeRaceId",
                table: "Motorbikes");

            migrationBuilder.DropTable(
                name: "MotorbikeRaces");

            migrationBuilder.DropIndex(
                name: "IX_Motorbikes_MotorbikeRaceId",
                table: "Motorbikes");

            migrationBuilder.DropColumn(
                name: "MotorbikeRaceId",
                table: "Motorbikes");
        }
    }
}
