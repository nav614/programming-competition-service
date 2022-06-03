﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProgrammingCompetitionService.Models;

#nullable disable

namespace ProgrammingCompetitionService.Migrations
{
    [DbContext(typeof(PCContext))]
    [Migration("20220530233400_AddRoleColumn")]
    partial class AddRoleColumn
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.5");

            modelBuilder.Entity("ProgrammingCompetitionService.Models.CompletedTaskItem", b =>
                {
                    b.Property<Guid>("CompletedTaskItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("ProgrammingLanguage")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("TaskItemId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("CompletedTaskItemId");

                    b.HasIndex("TaskItemId");

                    b.HasIndex("UserId");

                    b.ToTable("CompletedTasks");
                });

            modelBuilder.Entity("ProgrammingCompetitionService.Models.TaskItem", b =>
                {
                    b.Property<Guid>("TaskItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("InputParam")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("OutputParam")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("TaskItemId");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("ProgrammingCompetitionService.Models.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("BLOB");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("BLOB");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ProgrammingCompetitionService.Models.CompletedTaskItem", b =>
                {
                    b.HasOne("ProgrammingCompetitionService.Models.TaskItem", "TaskItem")
                        .WithMany()
                        .HasForeignKey("TaskItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProgrammingCompetitionService.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TaskItem");

                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}
