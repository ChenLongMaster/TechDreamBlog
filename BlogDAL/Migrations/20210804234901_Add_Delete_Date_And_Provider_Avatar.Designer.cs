﻿// <auto-generated />
using System;
using BlogDAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BlogDAL.Migrations
{
    [DbContext(typeof(BlogContext))]
    [Migration("20210804234901_Add_Delete_Date_And_Provider_Avatar")]
    partial class Add_Delete_Date_And_Provider_Avatar
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BlogDAL.Models.Article", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Abstract")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CreatedByNameId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("DisplayContent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.Property<string>("RepresentImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("CreatedByNameId");

                    b.ToTable("Article");
                });

            modelBuilder.Entity("BlogDAL.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Introduction")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Category");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedOn = new DateTime(2021, 8, 5, 6, 49, 0, 877, DateTimeKind.Local).AddTicks(5098),
                            Introduction = "<p>With a variety of topics to discuss,<i><strong> feel free to contribute your articles to my website.</strong></i></span></p>",
                            Name = "All Categories"
                        },
                        new
                        {
                            Id = 2,
                            CreatedOn = new DateTime(2021, 8, 5, 6, 49, 0, 878, DateTimeKind.Local).AddTicks(7961),
                            Introduction = "<p><strong>ASP.NET Core</strong> is the open-source version of ASP.NET, that runs on macOS, Linux, and Windows. ASP.NET Core was first released in 2016 and is a re-design of earlier Windows-only versions of ASP.NET.</p>",
                            Name = "ASP.NET Core"
                        },
                        new
                        {
                            Id = 3,
                            CreatedOn = new DateTime(2021, 8, 5, 6, 49, 0, 878, DateTimeKind.Local).AddTicks(7980),
                            Introduction = "<p><strong>Angular </strong>is a platform and framework for building single-page client applications using HTML and TypeScript.</p>",
                            Name = "Angular"
                        },
                        new
                        {
                            Id = 4,
                            CreatedOn = new DateTime(2021, 8, 5, 6, 49, 0, 878, DateTimeKind.Local).AddTicks(7983),
                            Introduction = "<p><strong>SQL </strong>stands for Structured Query Language. SQL is a standard language designed for managing data in a relational database management system.&nbsp;</p>",
                            Name = "SQL"
                        },
                        new
                        {
                            Id = 5,
                            CreatedOn = new DateTime(2021, 8, 5, 6, 49, 0, 878, DateTimeKind.Local).AddTicks(7985),
                            Introduction = "Blockchain is a system of recording information in a way that makes it difficult or impossible to change, hack, or cheat the system.",
                            Name = "Blockchain"
                        });
                });

            modelBuilder.Entity("BlogDAL.Models.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedOn")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("BlogDAL.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Avatar")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedOn")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Provider")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Salt")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("RoleUser", b =>
                {
                    b.Property<Guid>("RolesId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UsersId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("RolesId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("RoleUser");
                });

            modelBuilder.Entity("BlogDAL.Models.Article", b =>
                {
                    b.HasOne("BlogDAL.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BlogDAL.Models.User", "CreatedByName")
                        .WithMany()
                        .HasForeignKey("CreatedByNameId");

                    b.Navigation("Category");

                    b.Navigation("CreatedByName");
                });

            modelBuilder.Entity("RoleUser", b =>
                {
                    b.HasOne("BlogDAL.Models.Role", null)
                        .WithMany()
                        .HasForeignKey("RolesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BlogDAL.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
