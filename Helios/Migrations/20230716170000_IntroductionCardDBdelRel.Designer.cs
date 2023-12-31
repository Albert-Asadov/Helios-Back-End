﻿// <auto-generated />
using System;
using Helios.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Helios.Migrations
{
    [DbContext(typeof(HeliosDbContext))]
    [Migration("20230716170000_IntroductionCardDBdelRel")]
    partial class IntroductionCardDBdelRel
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.19")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Helios.Entities.Blog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("CardTitleDetail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ShortDescription")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<string>("TextContent")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TitleCard")
                        .IsRequired()
                        .HasMaxLength(19)
                        .HasColumnType("nvarchar(19)");

                    b.HasKey("Id");

                    b.ToTable("Blogs");
                });

            modelBuilder.Entity("Helios.Entities.BlogImages", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ImagePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("IsMain")
                        .HasColumnType("bit");

                    b.Property<int>("blogId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("blogId");

                    b.ToTable("BlogImages");
                });

            modelBuilder.Entity("Helios.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Helios.Entities.CategoryStyle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("categoryStyles");
                });

            modelBuilder.Entity("Helios.Entities.Component", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("URL")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("shortDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("textContent")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Components");
                });

            modelBuilder.Entity("Helios.Entities.ComponentCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<int>("ComponentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("ComponentId");

                    b.ToTable("ComponentCategories");
                });

            modelBuilder.Entity("Helios.Entities.ComponentImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ComponentId")
                        .HasColumnType("int");

                    b.Property<bool?>("IsMain")
                        .HasColumnType("bit");

                    b.Property<string>("imagePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ComponentId");

                    b.ToTable("ComponentImages");
                });

            modelBuilder.Entity("Helios.Entities.Introduction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("LeftSideTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RightSideTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Introductions");
                });

            modelBuilder.Entity("Helios.Entities.IntroductionCards", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("CardInsideText")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CardInsideTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("introductionCards");
                });

            modelBuilder.Entity("Helios.Entities.IntroductionImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool?>("IsMain")
                        .HasColumnType("bit");

                    b.Property<string>("imagePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("introductionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("introductionId");

                    b.ToTable("IntroductionImage");
                });

            modelBuilder.Entity("Helios.Entities.Setting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Key")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Key")
                        .IsUnique();

                    b.ToTable("Settings");
                });

            modelBuilder.Entity("Helios.Entities.Styles", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("URL")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("shortDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("textContent")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Styles");
                });

            modelBuilder.Entity("Helios.Entities.StylesCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CategoryStyleId")
                        .HasColumnType("int");

                    b.Property<int>("stylesId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoryStyleId");

                    b.HasIndex("stylesId");

                    b.ToTable("StylesCategories");
                });

            modelBuilder.Entity("Helios.Entities.StylesImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool?>("IsMain")
                        .HasColumnType("bit");

                    b.Property<string>("imagePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("stylesId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("stylesId");

                    b.ToTable("StylesImages");
                });

            modelBuilder.Entity("Helios.Entities.BlogImages", b =>
                {
                    b.HasOne("Helios.Entities.Blog", "blog")
                        .WithMany("blogImages")
                        .HasForeignKey("blogId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("blog");
                });

            modelBuilder.Entity("Helios.Entities.ComponentCategory", b =>
                {
                    b.HasOne("Helios.Entities.Category", "Category")
                        .WithMany("componentCategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Helios.Entities.Component", "Component")
                        .WithMany("componentCategories")
                        .HasForeignKey("ComponentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Component");
                });

            modelBuilder.Entity("Helios.Entities.ComponentImage", b =>
                {
                    b.HasOne("Helios.Entities.Component", "Component")
                        .WithMany("ComponentImages")
                        .HasForeignKey("ComponentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Component");
                });

            modelBuilder.Entity("Helios.Entities.IntroductionImage", b =>
                {
                    b.HasOne("Helios.Entities.Introduction", "introduction")
                        .WithMany("introductionImages")
                        .HasForeignKey("introductionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("introduction");
                });

            modelBuilder.Entity("Helios.Entities.StylesCategory", b =>
                {
                    b.HasOne("Helios.Entities.CategoryStyle", "CategoryStyle")
                        .WithMany("stylesCategories")
                        .HasForeignKey("CategoryStyleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Helios.Entities.Styles", "styles")
                        .WithMany("stylesCategories")
                        .HasForeignKey("stylesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CategoryStyle");

                    b.Navigation("styles");
                });

            modelBuilder.Entity("Helios.Entities.StylesImage", b =>
                {
                    b.HasOne("Helios.Entities.Styles", "styles")
                        .WithMany("stylesImages")
                        .HasForeignKey("stylesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("styles");
                });

            modelBuilder.Entity("Helios.Entities.Blog", b =>
                {
                    b.Navigation("blogImages");
                });

            modelBuilder.Entity("Helios.Entities.Category", b =>
                {
                    b.Navigation("componentCategories");
                });

            modelBuilder.Entity("Helios.Entities.CategoryStyle", b =>
                {
                    b.Navigation("stylesCategories");
                });

            modelBuilder.Entity("Helios.Entities.Component", b =>
                {
                    b.Navigation("ComponentImages");

                    b.Navigation("componentCategories");
                });

            modelBuilder.Entity("Helios.Entities.Introduction", b =>
                {
                    b.Navigation("introductionImages");
                });

            modelBuilder.Entity("Helios.Entities.Styles", b =>
                {
                    b.Navigation("stylesCategories");

                    b.Navigation("stylesImages");
                });
#pragma warning restore 612, 618
        }
    }
}
