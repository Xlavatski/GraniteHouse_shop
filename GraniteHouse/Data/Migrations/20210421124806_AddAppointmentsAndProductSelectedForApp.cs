using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GraniteHouse.Data.Migrations
{
    public partial class AddAppointmentsAndProductSelectedForApp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Appointmentss",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppointmentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CustomerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerPhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isConfirmed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointmentss", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductSelectedForAppointmentss",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppointmentId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSelectedForAppointmentss", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductSelectedForAppointmentss_Appointmentss_AppointmentId",
                        column: x => x.AppointmentId,
                        principalTable: "Appointmentss",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductSelectedForAppointmentss_Productss_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Productss",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductSelectedForAppointmentss_AppointmentId",
                table: "ProductSelectedForAppointmentss",
                column: "AppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSelectedForAppointmentss_ProductId",
                table: "ProductSelectedForAppointmentss",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductSelectedForAppointmentss");

            migrationBuilder.DropTable(
                name: "Appointmentss");
        }
    }
}
