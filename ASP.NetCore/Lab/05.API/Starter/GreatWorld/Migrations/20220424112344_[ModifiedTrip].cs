using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GreatWorld.Migrations
{
    public partial class ModifiedTrip : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stops_Trips_Tripid",
                table: "Stops");

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Trips",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "Tripid",
                table: "Stops",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Stops_Trips_Tripid",
                table: "Stops",
                column: "Tripid",
                principalTable: "Trips",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stops_Trips_Tripid",
                table: "Stops");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Trips");

            migrationBuilder.AlterColumn<int>(
                name: "Tripid",
                table: "Stops",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Stops_Trips_Tripid",
                table: "Stops",
                column: "Tripid",
                principalTable: "Trips",
                principalColumn: "id");
        }
    }
}
