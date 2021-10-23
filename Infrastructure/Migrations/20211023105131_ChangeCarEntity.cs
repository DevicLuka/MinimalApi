using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class ChangeCarEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MotorbikeRaceId",
                table: "Motorbikes",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "FinishedRace",
                table: "Cars",
                type: "bit",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "MotorbikeRaces",
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
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
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

            migrationBuilder.AlterColumn<int>(
                name: "FinishedRace",
                table: "Cars",
                type: "int",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");
        }
    }
}
