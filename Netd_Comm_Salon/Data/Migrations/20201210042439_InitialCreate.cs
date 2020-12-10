using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Netd_Comm_Salon.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "client",
                columns: table => new
                {
                    clientID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    clientFName = table.Column<string>(nullable: true),
                    clientLName = table.Column<string>(nullable: true),
                    clientPhonenumber = table.Column<string>(nullable: true),
                    clientEmail = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_client", x => x.clientID);
                });

            migrationBuilder.CreateTable(
                name: "stylist",
                columns: table => new
                {
                    stylistID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    stylistFName = table.Column<string>(nullable: true),
                    stylistLName = table.Column<string>(nullable: true),
                    stylistExt = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_stylist", x => x.stylistID);
                });

            migrationBuilder.CreateTable(
                name: "appointment",
                columns: table => new
                {
                    appointmentID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    appointmentDate = table.Column<DateTime>(nullable: false),
                    stylistID = table.Column<int>(nullable: false),
                    stylistFName = table.Column<string>(nullable: true),
                    stylistLName = table.Column<string>(nullable: true),
                    clientID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_appointment", x => x.appointmentID);
                    table.ForeignKey(
                        name: "FK_appointment_client_clientID",
                        column: x => x.clientID,
                        principalTable: "client",
                        principalColumn: "clientID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_appointment_stylist_stylistID",
                        column: x => x.stylistID,
                        principalTable: "stylist",
                        principalColumn: "stylistID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_appointment_clientID",
                table: "appointment",
                column: "clientID");

            migrationBuilder.CreateIndex(
                name: "IX_appointment_stylistID",
                table: "appointment",
                column: "stylistID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "appointment");

            migrationBuilder.DropTable(
                name: "client");

            migrationBuilder.DropTable(
                name: "stylist");
        }
    }
}
