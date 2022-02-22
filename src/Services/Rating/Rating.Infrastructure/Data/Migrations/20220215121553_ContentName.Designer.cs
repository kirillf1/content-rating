﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Rating.Infrastructure.Data;

#nullable disable

namespace Rating.Infrastructure.Migrations
{
    [DbContext(typeof(RatingDbContext))]
    [Migration("20220215121553_ContentName")]
    partial class ContentName
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Rating.Domain.Content", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<Guid?>("RoomId")
                        .HasColumnType("uuid");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RoomId");

                    b.ToTable("Content", (string)null);
                });

            modelBuilder.Entity("Rating.Domain.Room", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("CreatorId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsComplited")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsSingleRoom")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("Rating.Domain.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UserType")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("Rating.Domain.UserContentRating", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<long>("ContentId")
                        .HasColumnType("bigint");

                    b.Property<double>("Rating")
                        .HasColumnType("double precision");

                    b.HasKey("UserId", "ContentId");

                    b.HasIndex("ContentId");

                    b.ToTable("UserContentRatings");
                });

            modelBuilder.Entity("RoomUser", b =>
                {
                    b.Property<Guid>("RoomsId")
                        .HasColumnType("uuid");

                    b.Property<int>("UsersId")
                        .HasColumnType("integer");

                    b.HasKey("RoomsId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("RoomUser");
                });

            modelBuilder.Entity("Rating.Domain.Content", b =>
                {
                    b.HasOne("Rating.Domain.Room", null)
                        .WithMany("Contents")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Rating.Domain.UserContentRating", b =>
                {
                    b.HasOne("Rating.Domain.Content", "Content")
                        .WithMany("RatedByUsers")
                        .HasForeignKey("ContentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Rating.Domain.User", "User")
                        .WithMany("RatedContent")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Content");

                    b.Navigation("User");
                });

            modelBuilder.Entity("RoomUser", b =>
                {
                    b.HasOne("Rating.Domain.Room", null)
                        .WithMany()
                        .HasForeignKey("RoomsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Rating.Domain.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Rating.Domain.Content", b =>
                {
                    b.Navigation("RatedByUsers");
                });

            modelBuilder.Entity("Rating.Domain.Room", b =>
                {
                    b.Navigation("Contents");
                });

            modelBuilder.Entity("Rating.Domain.User", b =>
                {
                    b.Navigation("RatedContent");
                });
#pragma warning restore 612, 618
        }
    }
}
