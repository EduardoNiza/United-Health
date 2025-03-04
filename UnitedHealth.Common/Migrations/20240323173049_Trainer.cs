using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace UnitedHealth.Common.Migrations
{
    /// <inheritdoc />
    public partial class Trainer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<List<DateTime>>(
                name: "Doctor_FreeTimeSlots",
                table: "Users",
                type: "timestamp with time zone[]",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TrainerAppointments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PatientId = table.Column<int>(type: "integer", nullable: false),
                    TrainerId = table.Column<int>(type: "integer", nullable: false),
                    Scheduled = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Prescription = table.Column<string>(type: "text", nullable: false),
                    IsCompleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainerAppointments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainerAppointments_Users_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrainerAppointments_Users_TrainerId",
                        column: x => x.TrainerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TrainerAppointments_PatientId",
                table: "TrainerAppointments",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainerAppointments_TrainerId",
                table: "TrainerAppointments",
                column: "TrainerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TrainerAppointments");

            migrationBuilder.DropColumn(
                name: "Doctor_FreeTimeSlots",
                table: "Users");
        }
    }
}
