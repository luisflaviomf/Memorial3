﻿// <auto-generated />
using System;
using Memorial3.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Memorial3.Migrations
{
    [DbContext(typeof(Memorial3Context))]
    partial class Memorial3ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Memorial3.Models.CartItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CartId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("MemorialId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MemorialId");

                    b.ToTable("CartItems");
                });

            modelBuilder.Entity("Memorial3.Models.Memorial", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Coletivo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DataFalecimento")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataNascimento")
                        .HasColumnType("datetime2");

                    b.Property<string>("Formacao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Historia")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Hobbies")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MemorialPictureURL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Religiao")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Memorial");
                });

            modelBuilder.Entity("Memorial3.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("OrderPlaced")
                        .HasColumnType("datetime2");

                    b.Property<int>("OrderTotal")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Memorial3.Models.OrderItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("MemorialId")
                        .HasColumnType("int");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MemorialId");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderItems");
                });

            modelBuilder.Entity("Memorial3.Models.CartItem", b =>
                {
                    b.HasOne("Memorial3.Models.Memorial", "Memorial")
                        .WithMany()
                        .HasForeignKey("MemorialId");

                    b.Navigation("Memorial");
                });

            modelBuilder.Entity("Memorial3.Models.OrderItem", b =>
                {
                    b.HasOne("Memorial3.Models.Memorial", "Memorial")
                        .WithMany()
                        .HasForeignKey("MemorialId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Memorial3.Models.Order", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Memorial");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("Memorial3.Models.Order", b =>
                {
                    b.Navigation("OrderItems");
                });
#pragma warning restore 612, 618
        }
    }
}
