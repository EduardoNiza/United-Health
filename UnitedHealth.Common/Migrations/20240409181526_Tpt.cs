using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UnitedHealth.Common.Migrations
{
    /// <inheritdoc />
    public partial class Tpt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalAppointments_Users_DoctorId",
                table: "MedicalAppointments");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicalAppointments_Users_PatientId",
                table: "MedicalAppointments");

            migrationBuilder.DropForeignKey(
                name: "FK_NutritionalAppointments_Users_NutritionistId",
                table: "NutritionalAppointments");

            migrationBuilder.DropForeignKey(
                name: "FK_NutritionalAppointments_Users_PatientId",
                table: "NutritionalAppointments");

            migrationBuilder.DropForeignKey(
                name: "FK_TrainerAppointments_Users_PatientId",
                table: "TrainerAppointments");

            migrationBuilder.DropForeignKey(
                name: "FK_TrainerAppointments_Users_TrainerId",
                table: "TrainerAppointments");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Doctor_FreeTimeSlots",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "FreeTimeSlots",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Nutritionist_FreeTimeSlots",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Specialty",
                table: "Users");

            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    FreeTimeSlots = table.Column<List<DateTime>>(type: "timestamp with time zone[]", nullable: false),
                    Specialty = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Doctors_Users_Id",
                        column: x => x.Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Nutritionists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    FreeTimeSlots = table.Column<List<DateTime>>(type: "timestamp with time zone[]", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nutritionists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Nutritionists_Users_Id",
                        column: x => x.Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Patients_Users_Id",
                        column: x => x.Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Trainer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    FreeTimeSlots = table.Column<List<DateTime>>(type: "timestamp with time zone[]", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trainer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trainer_Users_Id",
                        column: x => x.Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalAppointments_Doctors_DoctorId",
                table: "MedicalAppointments",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalAppointments_Patients_PatientId",
                table: "MedicalAppointments",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NutritionalAppointments_Nutritionists_NutritionistId",
                table: "NutritionalAppointments",
                column: "NutritionistId",
                principalTable: "Nutritionists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NutritionalAppointments_Patients_PatientId",
                table: "NutritionalAppointments",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TrainerAppointments_Patients_PatientId",
                table: "TrainerAppointments",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TrainerAppointments_Trainer_TrainerId",
                table: "TrainerAppointments",
                column: "TrainerId",
                principalTable: "Trainer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalAppointments_Doctors_DoctorId",
                table: "MedicalAppointments");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicalAppointments_Patients_PatientId",
                table: "MedicalAppointments");

            migrationBuilder.DropForeignKey(
                name: "FK_NutritionalAppointments_Nutritionists_NutritionistId",
                table: "NutritionalAppointments");

            migrationBuilder.DropForeignKey(
                name: "FK_NutritionalAppointments_Patients_PatientId",
                table: "NutritionalAppointments");

            migrationBuilder.DropForeignKey(
                name: "FK_TrainerAppointments_Patients_PatientId",
                table: "TrainerAppointments");

            migrationBuilder.DropForeignKey(
                name: "FK_TrainerAppointments_Trainer_TrainerId",
                table: "TrainerAppointments");

            migrationBuilder.DropTable(
                name: "Doctors");

            migrationBuilder.DropTable(
                name: "Nutritionists");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "Trainer");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Users",
                type: "character varying(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<List<DateTime>>(
                name: "Doctor_FreeTimeSlots",
                table: "Users",
                type: "timestamp with time zone[]",
                nullable: true);

            migrationBuilder.AddColumn<List<DateTime>>(
                name: "FreeTimeSlots",
                table: "Users",
                type: "timestamp with time zone[]",
                nullable: true);

            migrationBuilder.AddColumn<List<DateTime>>(
                name: "Nutritionist_FreeTimeSlots",
                table: "Users",
                type: "timestamp with time zone[]",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Specialty",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalAppointments_Users_DoctorId",
                table: "MedicalAppointments",
                column: "DoctorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalAppointments_Users_PatientId",
                table: "MedicalAppointments",
                column: "PatientId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NutritionalAppointments_Users_NutritionistId",
                table: "NutritionalAppointments",
                column: "NutritionistId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NutritionalAppointments_Users_PatientId",
                table: "NutritionalAppointments",
                column: "PatientId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TrainerAppointments_Users_PatientId",
                table: "TrainerAppointments",
                column: "PatientId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TrainerAppointments_Users_TrainerId",
                table: "TrainerAppointments",
                column: "TrainerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
