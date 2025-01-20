using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CampusEventHubApi.Migrations
{
    /// <inheritdoc />
    public partial class AddKalendarTable2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kalendar",
                columns: table => new
                {
                    KalendarID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naslov = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Opis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Datum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Vrijeme = table.Column<TimeSpan>(type: "time", nullable: true),
                    Kreirano = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kalendar", x => x.KalendarID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Kalendar");
        }
    }
}
