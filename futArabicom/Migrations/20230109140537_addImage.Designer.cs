﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using futArabicom.Data;

#nullable disable

namespace futArabicom.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230109140537_addImage")]
    partial class addImage
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.1");

            modelBuilder.Entity("futArabicom.Models.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Club")
                        .HasColumnType("TEXT");

                    b.Property<int?>("Defending")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Dribbling")
                        .HasColumnType("INTEGER");

                    b.Property<byte[]>("Image")
                        .IsRequired()
                        .HasColumnType("BLOB");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("Pace")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Passing")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Physical")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Shooting")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Players");
                });
#pragma warning restore 612, 618
        }
    }
}
