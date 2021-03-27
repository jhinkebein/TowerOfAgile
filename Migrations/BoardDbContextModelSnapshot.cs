﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TowerOfAgile.Models;

namespace TowerOfAgile.Migrations
{
    [DbContext(typeof(BoardDbContext))]
    partial class BoardDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TowerOfAgile.Models.Board", b =>
                {
                    b.Property<int>("BoardId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Sharecode");

                    b.HasKey("BoardId");

                    b.ToTable("Boards");
                });

            modelBuilder.Entity("TowerOfAgile.Models.BoardElement", b =>
                {
                    b.Property<int>("BoardElementId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BoardId");

                    b.Property<string>("Element");

                    b.HasKey("BoardElementId");

                    b.HasIndex("BoardId");

                    b.ToTable("boardElements");
                });

            modelBuilder.Entity("TowerOfAgile.Models.BoardElement", b =>
                {
                    b.HasOne("TowerOfAgile.Models.Board", "Board")
                        .WithMany("Elements")
                        .HasForeignKey("BoardId");
                });
#pragma warning restore 612, 618
        }
    }
}
