using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PatientReservation.Migrations
{
    /// <inheritdoc />
    public partial class perawatan_foto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Foto",
                table: "Sembuh",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PerawatanId",
                table: "Sembuh",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Sembuh_PerawatanId",
                table: "Sembuh",
                column: "PerawatanId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sembuh_Perawatan_PerawatanId",
                table: "Sembuh",
                column: "PerawatanId",
                principalTable: "Perawatan",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sembuh_Perawatan_PerawatanId",
                table: "Sembuh");

            migrationBuilder.DropIndex(
                name: "IX_Sembuh_PerawatanId",
                table: "Sembuh");

            migrationBuilder.DropColumn(
                name: "Foto",
                table: "Sembuh");

            migrationBuilder.DropColumn(
                name: "PerawatanId",
                table: "Sembuh");
        }
    }
}
