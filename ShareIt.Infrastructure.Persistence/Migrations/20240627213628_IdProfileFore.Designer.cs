﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ShareIt.Infrastructure.Persistence;

#nullable disable

namespace ShareIt.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(DefaultContext))]
    [Migration("20240627213628_IdProfileFore")]
    partial class IdProfileFore
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.20")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ShareIt.Core.Domain.AppProfile", b =>
                {
                    b.Property<string>("IdUser")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("PhotoProfile")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdUser");

                    b.ToTable("Profiles", (string)null);
                });

            modelBuilder.Entity("ShareIt.Core.Domain.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<int?>("IdParentComment")
                        .HasColumnType("int");

                    b.Property<string>("IdProfile")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("IdPublication")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IdParentComment");

                    b.HasIndex("IdProfile");

                    b.HasIndex("IdPublication");

                    b.ToTable("Coments", (string)null);
                });

            modelBuilder.Entity("ShareIt.Core.Domain.Friendship", b =>
                {
                    b.Property<string>("AppProfileId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FriendId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("AppProfileId", "FriendId");

                    b.HasIndex("FriendId");

                    b.ToTable("Friendship", (string)null);
                });

            modelBuilder.Entity("ShareIt.Core.Domain.Publication", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Edited")
                        .HasColumnType("bit");

                    b.Property<string>("IdProfile")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Photo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VideoYoutube")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("IdProfile");

                    b.ToTable("Publications", (string)null);
                });

            modelBuilder.Entity("ShareIt.Core.Domain.Comment", b =>
                {
                    b.HasOne("ShareIt.Core.Domain.Comment", "ParentComment")
                        .WithMany("Replies")
                        .HasForeignKey("IdParentComment")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("ShareIt.Core.Domain.AppProfile", "Profile")
                        .WithMany("Comments")
                        .HasForeignKey("IdProfile")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ShareIt.Core.Domain.Publication", "publication")
                        .WithMany("Comments")
                        .HasForeignKey("IdPublication")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("ParentComment");

                    b.Navigation("Profile");

                    b.Navigation("publication");
                });

            modelBuilder.Entity("ShareIt.Core.Domain.Friendship", b =>
                {
                    b.HasOne("ShareIt.Core.Domain.AppProfile", "AppProfile")
                        .WithMany("Friends")
                        .HasForeignKey("AppProfileId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ShareIt.Core.Domain.AppProfile", "Friend")
                        .WithMany()
                        .HasForeignKey("FriendId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("AppProfile");

                    b.Navigation("Friend");
                });

            modelBuilder.Entity("ShareIt.Core.Domain.Publication", b =>
                {
                    b.HasOne("ShareIt.Core.Domain.AppProfile", "Profile")
                        .WithMany("Publications")
                        .HasForeignKey("IdProfile")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Profile");
                });

            modelBuilder.Entity("ShareIt.Core.Domain.AppProfile", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Friends");

                    b.Navigation("Publications");
                });

            modelBuilder.Entity("ShareIt.Core.Domain.Comment", b =>
                {
                    b.Navigation("Replies");
                });

            modelBuilder.Entity("ShareIt.Core.Domain.Publication", b =>
                {
                    b.Navigation("Comments");
                });
#pragma warning restore 612, 618
        }
    }
}
