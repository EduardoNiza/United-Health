﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using UnitedHealth.Common.Database;

#nullable disable

namespace UnitedHealth.Common.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240403002345_Nutritionist")]
    partial class Nutritionist
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("UnitedHealth.Common.Models.MedicalAppointment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("DoctorId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsCompleted")
                        .HasColumnType("boolean");

                    b.Property<int>("PatientId")
                        .HasColumnType("integer");

                    b.Property<string>("Prescription")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("Scheduled")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("DoctorId");

                    b.HasIndex("PatientId");

                    b.ToTable("MedicalAppointments");
                });

            modelBuilder.Entity("UnitedHealth.Common.Models.NutritionalAppointment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsCompleted")
                        .HasColumnType("boolean");

                    b.Property<int>("NutritionistId")
                        .HasColumnType("integer");

                    b.Property<int>("PatientId")
                        .HasColumnType("integer");

                    b.Property<string>("Prescription")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("Scheduled")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("NutritionistId");

                    b.HasIndex("PatientId");

                    b.ToTable("NutritionalAppointments");
                });

            modelBuilder.Entity("UnitedHealth.Common.Models.TrainerAppointment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsCompleted")
                        .HasColumnType("boolean");

                    b.Property<int>("PatientId")
                        .HasColumnType("integer");

                    b.Property<string>("Prescription")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("Scheduled")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("TrainerId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("PatientId");

                    b.HasIndex("TrainerId");

                    b.ToTable("TrainerAppointments");
                });

            modelBuilder.Entity("UnitedHealth.Common.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("character varying(13)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("Users");

                    b.HasDiscriminator<string>("Discriminator").HasValue("User");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("UnitedHealth.Common.Models.Doctor", b =>
                {
                    b.HasBaseType("UnitedHealth.Common.Models.User");

                    b.Property<List<DateTime>>("FreeTimeSlots")
                        .IsRequired()
                        .HasColumnType("timestamp with time zone[]");

                    b.Property<string>("Specialty")
                        .IsRequired()
                        .HasColumnType("text");

                    b.ToTable("Users", t =>
                        {
                            t.Property("FreeTimeSlots")
                                .HasColumnName("Doctor_FreeTimeSlots");
                        });

                    b.HasDiscriminator().HasValue("Doctor");
                });

            modelBuilder.Entity("UnitedHealth.Common.Models.Nutritionist", b =>
                {
                    b.HasBaseType("UnitedHealth.Common.Models.User");

                    b.Property<List<DateTime>>("FreeTimeSlots")
                        .IsRequired()
                        .HasColumnType("timestamp with time zone[]");

                    b.ToTable("Users", t =>
                        {
                            t.Property("FreeTimeSlots")
                                .HasColumnName("Nutritionist_FreeTimeSlots");
                        });

                    b.HasDiscriminator().HasValue("Nutritionist");
                });

            modelBuilder.Entity("UnitedHealth.Common.Models.Patient", b =>
                {
                    b.HasBaseType("UnitedHealth.Common.Models.User");

                    b.HasDiscriminator().HasValue("Patient");
                });

            modelBuilder.Entity("UnitedHealth.Common.Models.Trainer", b =>
                {
                    b.HasBaseType("UnitedHealth.Common.Models.User");

                    b.Property<List<DateTime>>("FreeTimeSlots")
                        .IsRequired()
                        .HasColumnType("timestamp with time zone[]");

                    b.HasDiscriminator().HasValue("Trainer");
                });

            modelBuilder.Entity("UnitedHealth.Common.Models.MedicalAppointment", b =>
                {
                    b.HasOne("UnitedHealth.Common.Models.Doctor", "Doctor")
                        .WithMany()
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UnitedHealth.Common.Models.Patient", "Patient")
                        .WithMany()
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Doctor");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("UnitedHealth.Common.Models.NutritionalAppointment", b =>
                {
                    b.HasOne("UnitedHealth.Common.Models.Nutritionist", "Nutritionist")
                        .WithMany()
                        .HasForeignKey("NutritionistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UnitedHealth.Common.Models.Patient", "Patient")
                        .WithMany()
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Nutritionist");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("UnitedHealth.Common.Models.TrainerAppointment", b =>
                {
                    b.HasOne("UnitedHealth.Common.Models.Patient", "Patient")
                        .WithMany()
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UnitedHealth.Common.Models.Trainer", "Trainer")
                        .WithMany()
                        .HasForeignKey("TrainerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Patient");

                    b.Navigation("Trainer");
                });
#pragma warning restore 612, 618
        }
    }
}
