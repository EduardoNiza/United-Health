using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace UnitedHealth.Common.Migrations
{
    /// <inheritdoc />
    public partial class Nutritionist : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Discriminator",
                table: "Users",
                type: "character varying(13)",
                maxLength: 13,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(8)",
                oldMaxLength: 8);

            migrationBuilder.AddColumn<List<DateTime>>(
                name: "Nutritionist_FreeTimeSlots",
                table: "Users",
                type: "timestamp with time zone[]",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "NutritionalAppointments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PatientId = table.Column<int>(type: "integer", nullable: false),
                    NutritionistId = table.Column<int>(type: "integer", nullable: false),
                    Scheduled = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Prescription = table.Column<string>(type: "text", nullable: false),
                    IsCompleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NutritionalAppointments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NutritionalAppointments_Users_NutritionistId",
                        column: x => x.NutritionistId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NutritionalAppointments_Users_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NutritionalAppointments_NutritionistId",
                table: "NutritionalAppointments",
                column: "NutritionistId");

            migrationBuilder.CreateIndex(
                name: "IX_NutritionalAppointments_PatientId",
                table: "NutritionalAppointments",
                column: "PatientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NutritionalAppointments");

            migrationBuilder.DropColumn(
                name: "Nutritionist_FreeTimeSlots",
                table: "Users");

            migrationBuilder.AlterColumn<string>(
                name: "Discriminator",
                table: "Users",
                type: "character varying(8)",
                maxLength: 8,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(13)",
                oldMaxLength: 13);
        }
    }
}
