using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CampusEventHubApi.Migrations
{
    /// <inheritdoc />
    public partial class AddOcjeneTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ocjene",
                columns: table => new
                {
                    OcjenaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    OcjenaVrijednost = table.Column<int>(type: "int", nullable: false),
                    Komentar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Kreirano = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ocjene", x => x.OcjenaID);
                    table.ForeignKey(
                        name: "FK_Ocjene_Event_EventID",
                        column: x => x.EventID,
                        principalTable: "Event",
                        principalColumn: "IDEvent",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ocjene_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "IDUser",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ocjene_EventID",
                table: "Ocjene",
                column: "EventID");

            migrationBuilder.CreateIndex(
                name: "IX_Ocjene_UserID",
                table: "Ocjene",
                column: "UserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ocjene");
        }
    }
}
