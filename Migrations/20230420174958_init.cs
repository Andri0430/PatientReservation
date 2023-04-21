using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PatientReservation.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admin",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admin", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Alamat",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Kota = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Jalan = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alamat", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Perawatan",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NamaPerawatan = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Perawatan", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Stat = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Kamar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NamaKamar = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PerawatanId = table.Column<int>(type: "int", nullable: false),
                    Terisi = table.Column<int>(type: "int", nullable: false),
                    Kuota = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kamar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Kamar_Perawatan_PerawatanId",
                        column: x => x.PerawatanId,
                        principalTable: "Perawatan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pasien",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nama = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Umur = table.Column<int>(type: "int", maxLength: 2, nullable: false),
                    TempatLahir = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TanggalLahir = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NoTelepon = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    NoKtp = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    Foto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AlamatId = table.Column<int>(type: "int", nullable: false),
                    PerawatanId = table.Column<int>(type: "int", nullable: false),
                    KamarID = table.Column<int>(type: "int", nullable: false),
                    TanggalMasuk = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TanggalKeluar = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StatId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pasien", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pasien_Alamat_AlamatId",
                        column: x => x.AlamatId,
                        principalTable: "Alamat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pasien_Perawatan_PerawatanId",
                        column: x => x.PerawatanId,
                        principalTable: "Perawatan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pasien_Status_StatId",
                        column: x => x.StatId,
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Kamar_PerawatanId",
                table: "Kamar",
                column: "PerawatanId");

            migrationBuilder.CreateIndex(
                name: "IX_Pasien_AlamatId",
                table: "Pasien",
                column: "AlamatId");

            migrationBuilder.CreateIndex(
                name: "IX_Pasien_PerawatanId",
                table: "Pasien",
                column: "PerawatanId");

            migrationBuilder.CreateIndex(
                name: "IX_Pasien_StatId",
                table: "Pasien",
                column: "StatId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admin");

            migrationBuilder.DropTable(
                name: "Kamar");

            migrationBuilder.DropTable(
                name: "Pasien");

            migrationBuilder.DropTable(
                name: "Alamat");

            migrationBuilder.DropTable(
                name: "Perawatan");

            migrationBuilder.DropTable(
                name: "Status");
        }
    }
}
